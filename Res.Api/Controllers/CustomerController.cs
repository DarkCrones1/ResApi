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
public class CustomerController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICrudService<Customer> _service;
    private readonly TokenHelper _tokenHelper;

    public CustomerController(IMapper mapper, ICrudService<Customer> service, TokenHelper tokenHelper)
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
        var filters = _mapper.Map<Customer>(filter);
        var entities = await _service.GetPaged(filters);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );

        var dtos = _mapper.Map<IEnumerable<CustomerResponseDto>>(entities);
        var response = new ApiResponse<IEnumerable<CustomerResponseDto>>(data: dtos, meta:metaDataResponse);
        return Ok(response);
        
    }

    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CustomerResponseDto>))]
    public async Task<IActionResult> Create([FromBody] CustomerCreateRequestDto requestDto)
    {
        var customer = _mapper.Map<Customer>(requestDto);

        var customerAddress = _mapper.Map<CustomerAddress>(requestDto);
        customerAddress.Status = 1;
        customer.CustomerAddress.Add(customerAddress);

        await _service.Create(customer);

        var dto = _mapper.Map<CustomerResponseDto>(customer);
        var response = new ApiResponse<CustomerResponseDto>(data: dto);
        return  Ok(response);

    }
}