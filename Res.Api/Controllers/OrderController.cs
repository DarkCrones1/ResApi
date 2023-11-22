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
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICrudService<Order> _service;
    private readonly ICatalogBaseService<Drink> _drinkService;
    private readonly ICatalogBaseService<Food> _foodService;

    public OrderController(IMapper mapper, ICrudService<Order> service, ICatalogBaseService<Drink> drinkService, ICatalogBaseService<Food> foodService)
    {
        this._mapper = mapper;
        this._service = service;
        this._drinkService = drinkService;
        this._foodService = foodService;
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<OrderResponseDto>))]
    public async Task<IActionResult> Create([FromBody] OrderCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Order>(requestDto);
        await _service.Create(entity);

        var dto = _mapper.Map<OrderResponseDto>(entity);
        var response = new ApiResponse<OrderResponseDto>(data: dto);
        return Ok(response);
    }
}