using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Res.Common.Interfaces.Services;
using Res.Domain.Dto.Response;
using Res.Domain.Entities;
using Res.API.Responses;
using Res.Domain.Dto.Request.Create;
using Res.Common.Exceptions;
using Res.Api.Helper;
using Microsoft.AspNetCore.Authorization;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Interfaces.Services;
using Res.Domain.Dto.Request;
using Res.API.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Res.Common.Functions;

namespace Res.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LoginController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _service;
    private readonly IUserAccountService _userAccountService;
    private readonly IConfiguration _configuration;
    protected ActiveUserAccountEmployee _user;
    protected IEnumerable<BranchStore> _lstBranchStoresToUserAccount;

    public LoginController(IMapper mapper, IAuthenticationService service, IUserAccountService userAccountService, IConfiguration configuration)
    {
        this._mapper = mapper;
        this._service = service;
        this._userAccountService = userAccountService;
        this._configuration = configuration;
        _user = new ActiveUserAccountEmployee();
        _lstBranchStoresToUserAccount = new List<BranchStore>();
        SettingConfigurationFile.Initialize(_configuration);
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] LoginRequestDto requestDto)
    {
        var result = await _service.IsValidUser(requestDto.UserName!, MD5Encrypt.GetMD5(requestDto.Password!));

        if (!result)
            return NotFound("El usuario no es válido");

        _user = await GetEmployee(requestDto);

        await GetBranchStoresToUserAccount();

        //TODO: falta agregar la parte de administrador, en ese caso no se tiene asignada una sucursal
        if (!_lstBranchStoresToUserAccount.Any())
            return NotFound("No existe ninguna relaacion con sucursal");

        var token = await GenerateToken();

        return Ok(new { token });
    }

    private async Task<string> GenerateToken()
    {
        // Header
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var lstClaims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, _user!.UserName),
                new Claim(ClaimTypes.Name, _user!.Name),
                new Claim(ClaimTypes.PrimaryGroupSid, $"{_user.JobId}"),
                new Claim(ClaimTypes.Role, $"{_user.JobName}"),
                new Claim(ClaimTypes.Email, _user.Email),
                new Claim(ClaimTypes.Sid, $"{_user.Id}"),
                new Claim(ClaimTypes.DateOfBirth, DateTime.Now.ToString()),
                //TODO: obtener todas las sucursales donde trabaja el usuario de momento obtenemos y asignamos la primera
                new Claim(ClaimTypes.GroupSid, $"{_lstBranchStoresToUserAccount.FirstOrDefault()?.Id}"),
                new Claim("UserAccountType", $"{_user.AccountType}"),
                new Claim("EmployeeId", $"{_user.EmployeeId}"),
                new Claim("BranchStoreIds", $"{string.Join(",", _lstBranchStoresToUserAccount.Select(x => x.Id))}"),
                //new Claim("", "") //TODO: agregar valores personalizados
            };

        // Payload
        var elapsedTime = int.Parse(_configuration["Authentication:ExpirationMinutes"]!);

        var payload = new JwtPayload(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: lstClaims,
            notBefore: DateTime.Now,
            expires: DateTime.UtcNow.AddMinutes(elapsedTime)
        );

        // Signature
        var token = new JwtSecurityToken(header, payload);

        await Task.CompletedTask;

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<ActiveUserAccountEmployee> GetEmployee(LoginRequestDto requestDto)
    {
        Expression<Func<ActiveUserAccountEmployee, bool>> filters = x => x.UserName == requestDto.UserName;
        var entity = await _userAccountService.GetUserAccountToLogin(filters);
        return entity;
    }

    private async Task GetBranchStoresToUserAccount()
    {
        _lstBranchStoresToUserAccount = await _userAccountService.GetBranchStoresToUserAccount(_user.Id);
    }

    [HttpPost]
    [Route("AuthenticateUser")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActiveUserAccountEmployee))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ActiveUserAccountEmployee))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ActiveUserAccountEmployee))]
    public async Task<IActionResult> AuthenticateUser([FromBody] LoginRequestDto requestDto)
    {
        var result = await _service.IsValidUser(requestDto.UserName!, MD5Encrypt.GetMD5(requestDto.Password!));

        if (!result)
            return Unauthorized();

        Expression<Func<ActiveUserAccountEmployee, bool>> filters = x => x.UserName == requestDto.UserName;

        var entity = await _userAccountService.GetUserAccountToLogin(filters);

        if (entity.Id == 0)
            return Unauthorized();

        var dto = _mapper.Map<ActiveUserAccountEmployee>(entity);

        return Ok(dto);
    }

    [HttpPost]
    [Route("AuthorizeTransaction")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ActiveUserAccountEmployee))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ActiveUserAccountEmployee))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ActiveUserAccountEmployee))]
    public async Task<IActionResult> AuthorizeTransaction([FromBody] LoginRequestDto requestDto)
    {
        var result = await _service.IsValidUser(requestDto.UserName!, MD5Encrypt.GetMD5(requestDto.Password!));

        if (!result)
            return Unauthorized();

        var administratorJobId = SettingConfigurationFile.Instance.AdministratorRolId;

        Expression<Func<ActiveUserAccountEmployee, bool>> filters = x => x.UserName == requestDto.UserName && x.AccountType == administratorJobId;

        var entity = await _userAccountService.GetUserAccountToLogin(filters);

        if (entity.Id == 0)
            return Unauthorized();

        var dto = _mapper.Map<ActiveUserAccountEmployee>(entity);

        return Ok(dto);
    }
}
