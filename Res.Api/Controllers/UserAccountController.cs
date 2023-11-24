using System.Linq.Expressions;
using System.Net;

using Microsoft.AspNetCore.Mvc;

using AutoMapper;

using Res.Common.Interfaces.Services;
using Res.Domain.Dto.Response;
using Res.Domain.Entities;
using Res.API.Responses;
using Res.Domain.Dto.Request.Create;
using Res.Domain.Dto.Request.Update;
using Res.Common.Exceptions;
using Res.Api.Helper;
using Microsoft.AspNetCore.Authorization;
using Res.Domain.Dto.QueryFilters;
using Res.Domain.Interfaces.Services;
using Res.Common.QueryFilters;
using Res.Domain.Enumerations;
using Res.Common.Functions;

namespace Res.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class UserAccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserAccountService _service;
    private readonly ICrudService<Rol> _rolService;
    private readonly IBranchStoreService _branchStoreService;
    private readonly TokenHelper _tokenHelper;
    private readonly IEmployeeService _employeeService;

    public UserAccountController(IMapper mapper, IUserAccountService service, ICrudService<Rol> rolService, IBranchStoreService branchService, TokenHelper tokenHelper, IEmployeeService employeeService)
    {
        this._rolService = rolService;
        this._branchStoreService = branchService;
        this._tokenHelper = tokenHelper;
        this._employeeService = employeeService;
        this._mapper = mapper;
        this._service = service;
    }

    [HttpGet]
    [Route("Employee")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserAccountResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<UserAccountResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<UserAccountResponseDto>>))]
    public async Task<IActionResult> GetUser([FromQuery] UserAccountQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<UserAccountResponseDto>>(entities);

        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<UserAccountResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpGet]
    [Route("Customer")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<UserAccountCustomerResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<UserAccountCustomerResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<UserAccountCustomerResponseDto>>))]
    public async Task<IActionResult> GetUserAcustomer([FromQuery] UserAccountCustomerQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<UserAccountCustomerResponseDto>>(entities);

        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<UserAccountCustomerResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpPost]
    [Route("Employee")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserAccountResponseDto>))]
    public async Task<IActionResult> CreateUser([FromBody] UserAccountCreateRequestDto requestDto)
    {
        try
        {
            Expression<Func<UserAccount, bool>> filter = x => x.UserName == requestDto.UserName && !x.IsDeleted!.Value;

            var existUser = await _service.Exist(filter);

            if (existUser)
                return BadRequest("Ya existe un usuario con este nombre de usuario");

            var entity = new UserAccount();

            if (requestDto.EmployeeId.HasValue && requestDto.EmployeeId > 0)
            {
                entity = await AssingUserAccount(requestDto);
            }
            else
            {
                entity = await PopulateUserAccount(requestDto);
            }

            entity.Password = MD5Encrypt.GetMD5(requestDto.Password);
            await _service.CreateUser(entity);

            var result = _mapper.Map<UserAccountResponseDto>(entity);
            var response = new ApiResponse<UserAccountResponseDto>(result);
            return Ok(response);
        }
        catch (Exception ex)
        {
            throw new LogicBusinessException(ex);
        }
    }

    [HttpPost]
    [Route("Customer")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<UserAccountCustomerResponseDto>))]
    public async Task<IActionResult> CreateUserCustomer([FromBody] UserAccountCustomerCreateRequestDto requestDto)
    {
        Expression<Func<UserAccount, bool>> filter = x => x.UserName == requestDto.UserName && !x.IsDeleted!.Value;

        var existUser = await _service.Exist(filter);

        if (existUser)
            return BadRequest("Ya existe un usuario con este nombre de usuario");

        var entity = await PopulateUserAccountCustomer(requestDto);
        entity.Password = MD5Encrypt.GetMD5(requestDto.Password);
        await _service.CreateUser(entity);

        var result = _mapper.Map<UserAccountCustomerResponseDto>(entity);
        var response = new ApiResponse<UserAccountCustomerResponseDto>(result);
        return Ok(response);
    }


    private async Task<Rol> GetRolToNewUserAccount(int rolId)
    {
        var rol = await _rolService.GetById(rolId);
        return rol;
    }

    private async Task<BranchStore> GetBranchStoreToNewUserAccount(int branchstoreId)
    {
        var branchStore = await _branchStoreService.GetById(branchstoreId);
        return branchStore;
    }

    private async Task<UserAccount> PopulateUserAccount(UserAccountCreateRequestDto requestDto)
    {
        var userAccount = _mapper.Map<UserAccount>(requestDto);

        var employee = _mapper.Map<Employee>(requestDto, opts => opts.Items["CreatedUser"] = _tokenHelper.GetUserName());
        employee.UserAccount.Add(userAccount);

        userAccount.Employee.Add(employee);

        if (requestDto.RolIds != null)
        {
            foreach (var item in requestDto.RolIds)
            {
                var rol = await GetRolToNewUserAccount(item);
                if (rol.Id > 0)
                    userAccount.Rol.Add(rol);
            }
        }

        if (requestDto.BranchStoreIds != null)
        {
            foreach (var item in requestDto.BranchStoreIds)
            {
                var branchStore = await GetBranchStoreToNewUserAccount(item);
                if (branchStore.Id > 0)
                    userAccount.BranchStore.Add(branchStore);
            }
        }

        return userAccount;
    }

    private async Task<UserAccount> AssingUserAccount(UserAccountCreateRequestDto requestDto)
    {
        var userAccount = _mapper.Map<UserAccount>(requestDto);

        var employee = await _employeeService.GetById(requestDto.EmployeeId!.Value);

        employee.UserAccount.Add(userAccount);
        userAccount.Employee.Add(employee);

        if (requestDto.RolIds != null)
        {
            foreach (var item in requestDto.RolIds)
            {
                var rol = await GetRolToNewUserAccount(item);
                if (rol.Id > 0)
                    userAccount.Rol.Add(rol);
            }
        }

        if (requestDto.BranchStoreIds != null)
        {
            foreach (var item in requestDto.BranchStoreIds)
            {
                var branchStore = await GetBranchStoreToNewUserAccount(item);
                if (branchStore.Id > 0)
                    userAccount.BranchStore.Add(branchStore);
            }
        }

        return userAccount;
    }

    private async Task<UserAccount> PopulateUserAccountCustomer(UserAccountCustomerCreateRequestDto requestDto)
    {
        var userAccount = _mapper.Map<UserAccount>(requestDto);

        var customer = _mapper.Map<Customer>(requestDto);
        customer.UserAccount.Add(userAccount);

        userAccount.Customer.Add(customer);

        if (requestDto.BranchStoreIds != null)
        {
            foreach (var item in requestDto.BranchStoreIds)
            {
                var branchStore = await GetBranchStoreToNewUserAccount(item);
                if (branchStore.Id > 0)
                    userAccount.BranchStore.Add(branchStore);
            }
        }

        return userAccount;
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, UserAccountUpdateRequestDto requestDto)
    {
        Expression<Func<UserAccount, bool>> filter = x => x.Id == id;

        var existUser = await _service.Exist(filter);

        if (!existUser)
            return BadRequest("No se encontró el usuario con esas características");

        var entity = await _service.GetById(id);
        entity.Id = id;
        entity.Password = requestDto.Password;
        entity.IsDeleted = false;

        await _service.Update(entity);
        return Ok(true);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Expression<Func<UserAccount, bool>> filter = x => x.Id == id;

            var existUserAccount = await _service.Exist(filter);

            if (!existUserAccount)
                return BadRequest("No existe el usuario que intenta eliminar");

            var newEntity = await _service.GetById(id);
            newEntity.IsDeleted = true;
            await _service.Update(newEntity);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }
}