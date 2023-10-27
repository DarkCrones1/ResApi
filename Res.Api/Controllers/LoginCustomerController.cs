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

namespace Res.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class LoginCustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthenticationService _service;
    private readonly IUserAccountService _userAccountService;
    private readonly IConfiguration _configuration;
    protected ActiveUserAccountCustomer _user;
    protected IEnumerable<BranchStore> _lstBranchStoresToUserAccount;

    public LoginCustomerController(IMapper mapper, IAuthenticationService service, IUserAccountService userAccountService, IConfiguration configuration)
    {
        this._mapper = mapper;
        this._service = service;
        this._userAccountService = userAccountService;
        this._configuration = configuration;
        _user = new ActiveUserAccountCustomer();
        _lstBranchStoresToUserAccount = new List<BranchStore>();
        SettingConfigurationFile.Initialize(_configuration);
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] LoginRequestDto requestDto)
    {
        var result = await _service.IsValidUser(requestDto.UserName!, requestDto.Password!);

        if (!result)
            return NotFound();

        _user = await GetCustomer(requestDto);

        await GetBranchStoresToUserAccount();

        if (!_lstBranchStoresToUserAccount.Any())
            return NotFound();

        var token = await GenerateToken();

        return Ok(new {token});
    }

    private async Task<string> GenerateToken()
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        var header = new JwtHeader(signingCredentials);

        var lstClaims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, _user!.UserName),
                new Claim(ClaimTypes.Name, _user!.Name),
                new Claim(ClaimTypes.PrimaryGroupSid, $"{_user.RolId}"),
                new Claim(ClaimTypes.Email, _user.Email),
                new Claim(ClaimTypes.Sid, $"{_user.Id}"),
                new Claim(ClaimTypes.DateOfBirth, DateTime.Now.ToString()),
                //TODO: obtener todas las sucursales donde trabaja el usuario de momento obtenemos y asignamos la primera
                new Claim(ClaimTypes.GroupSid, $"{_lstBranchStoresToUserAccount.FirstOrDefault()?.Id}"),
                new Claim("CustomerId", $"{_user.CustomerId}"),
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

    private async Task<ActiveUserAccountCustomer> GetCustomer(LoginRequestDto requestDto)
    {
        Expression<Func<ActiveUserAccountCustomer, bool>> filters = x => x.UserName == requestDto.UserName;
        var entity = await _userAccountService.GetUserAccountCustomerToLogin(filters);
        return entity;
    }

    private async Task GetBranchStoresToUserAccount()
    {
        _lstBranchStoresToUserAccount = await _userAccountService.GetBranchStoresToUserAccount(_user.Id);
    }
}