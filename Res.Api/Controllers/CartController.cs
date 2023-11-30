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
using Res.Domain.Enumerations;

namespace Res.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICrudService<Cart> _service;
    private readonly TokenHelper _tokenHelper;
    private readonly ICatalogBaseService<Food> _foodService;
    private readonly ICatalogBaseService<Drink> _drinkService;
    private readonly ICrudService<Order> _orderService;

    public CartController(IMapper mapper, ICrudService<Cart> service, TokenHelper tokenHelper, ICatalogBaseService<Food> foodService, ICatalogBaseService<Drink> drinkService, ICrudService<Order> orderService)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._foodService = foodService;
        this._drinkService = drinkService;
        this._orderService = orderService;
    }

    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CartResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CartResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CartResponseDto>>))]
    public async Task<IActionResult> Get([FromQuery] CartQueryFilter filter)
    {
        var filters = _mapper.Map<Cart>(filter);
        var entities = await _service.GetPaged(filters);
        var dtos = _mapper.Map<IEnumerable<CartResponseDto>>(entities);

        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<CartResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CartDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CartDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CartDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Expression<Func<Cart, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);

        var dto = _mapper.Map<CartDetailResponseDto>(entity);
        var response = new ApiResponse<CartDetailResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CartResponseDto>))]
    public async Task<IActionResult> Create([FromBody] CartCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Cart>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();

        if (requestDto.DrinkIds != null)
        {
            foreach (var item in requestDto.DrinkIds)
            {
                var drink = await _drinkService.GetById(item);
                if (drink.Id > 0)
                    entity.Drink.Add(drink);
            }
        }

        if (requestDto.FoodIds != null)
        {
            foreach (var item in requestDto.FoodIds)
            {
                var food = await _foodService.GetById(item);
                if (food.Id > 0)
                    entity.Food.Add(food);
            }
        }
        await _service.Create(entity);

        var entityOrder = _mapper.Map<Order>(requestDto);
        entityOrder.CartId = entity.Id;
        await _orderService.Create(entityOrder);

        var cartDto = _mapper.Map<CartResponseDto>(entity);
        var orderDto = _mapper.Map<OrderResponseDto>(entityOrder);

        var responseDto = new
        {
            CartResponse = new ApiResponse<CartResponseDto>(data: cartDto),
            OrderResponse = new ApiResponse<OrderResponseDto>(data: orderDto)
        };
        return Ok(responseDto);
    }

    [HttpDelete]
    [Route("{id:int}/Cancel")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CartResponseDto>))]
    public async Task<IActionResult> CancelOrder([FromRoute] int id)
    {
        Expression<Func<Cart, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);
        entity.Status = (short)CartStatus.Canceled;
        await _service.Update(entity);

        var dto = _mapper.Map<CartResponseDto>(entity);
        var response = new ApiResponse<CartResponseDto>(data: dto);
        return Ok(response);
    }
}