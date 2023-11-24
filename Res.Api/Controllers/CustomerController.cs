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

namespace Res.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class CustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICustomerService _service;
    private readonly TokenHelper _tokenHelper;

    public CustomerController(IMapper mapper, ICustomerService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CustomerResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CustomerResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CustomerResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] CustomerQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );

        var dtos = _mapper.Map<IEnumerable<CustomerResponseDto>>(entities);
        var response = new ApiResponse<IEnumerable<CustomerResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CustomerResponseDto>))]
    public async Task<IActionResult> Create([FromBody] CustomerCreateRequestDto requestDto)
    {
        try
        {
            var customer = _mapper.Map<Customer>(requestDto);
            customer.CreatedBy = "Admin";

            await _service.Create(customer);

            var dto = _mapper.Map<CustomerResponseDto>(customer);
            var response = new ApiResponse<CustomerResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CustomerUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Customer, bool>> filter = x => x.Id == id;
            var existCustomer = await _service.Exist(filter);
    
            if (!existCustomer)
                return BadRequest("No existe ningun empleado con esa información");
    
            var newEntity = _mapper.Map<Customer>(requestDto);
    
            newEntity.IsDeleted = false;
            newEntity.Id = id;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();
            newEntity.LastModifiedDate = DateTime.Now;
    
            await _service.Update(id, newEntity);
    
            var dto = _mapper.Map<CustomerResponseDto>(newEntity);
            var response = new ApiResponse<CustomerResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            
            throw new LogicBusinessException(ex);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
    {
        try
        {
            Expression<Func<Customer, bool>> filter = x => x.Id == id;
            var existCustomer = await _service.Exist(filter);
    
            if (!existCustomer)
                return BadRequest("No existe ningun empleado con esa información");

            var entity = await _service.GetById(id);
            entity.IsDeleted = true;

            await _service.Update(entity);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }
}