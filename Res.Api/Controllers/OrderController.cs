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
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICrudService<Order> _service;
    private readonly ICrudService<OrderDrink> _drinkService;
    private readonly ICrudService<OrderFood> _foodService;
    private readonly ICrudService<Cart> _cartService;
    private readonly TokenHelper _tokenHelper;

    public OrderController(IMapper mapper, ICrudService<Order> service, ICrudService<OrderDrink> drinkService, ICrudService<OrderFood> foodService, ICrudService<Cart> cartService, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._drinkService = drinkService;
        this._foodService = foodService;
        this._cartService = cartService;
        this._tokenHelper = tokenHelper;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ReservationResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<ReservationResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<ReservationResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] OrderQueryFilter filter)
    {
        var filters = _mapper.Map<Order>(filter);
        var entities = await _service.GetPaged(filters);
        var dtos = _mapper.Map<IEnumerable<OrderResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );

        var response = new ApiResponse<IEnumerable<OrderResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<OrderDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<OrderDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<OrderDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Expression<Func<Order, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);

        var dto = _mapper.Map<OrderDetailResponseDto>(entity);
        var response = new ApiResponse<OrderDetailResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}/OrderDrink")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<OrderDrinkResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<OrderDrinkResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<OrderDrinkResponseDto>>))]
    public async Task<IActionResult> GetOrderDrink([FromRoute] int id)
    {
        Expression<Func<Order, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        Expression<Func<OrderDrink, bool>> filterD = x => x.OrderId == id;

        var entities = await _drinkService.GetBy(filterD);
        var dto = _mapper.Map<IEnumerable<OrderDrinkResponseDto>>(entities);
        var response = new ApiResponse<IEnumerable<OrderDrinkResponseDto>>(data: dto);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}/OrderFood")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<OrderFoodResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<OrderFoodResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<OrderFoodResponseDto>>))]
    public async Task<IActionResult> GetOrderFood([FromRoute] int id)
    {
        Expression<Func<Order, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        Expression<Func<OrderFood, bool>> filterF = x => x.OrderId == id;

        var entities = await _foodService.GetBy(filterF);
        var dto = _mapper.Map<IEnumerable<OrderFoodResponseDto>>(entities);
        var response = new ApiResponse<IEnumerable<OrderFoodResponseDto>>(data: dto);
        return Ok(response);
    }

    [HttpPost]
    [Route("{cartId:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<OrderResponseDto>))]
    public async Task<IActionResult> Create([FromBody] OrderCreateRequestDto requestDto, [FromRoute] int cartId)
    {
        var entity = _mapper.Map<Order>(requestDto);
        await _service.Create(entity);

        var cart = await _cartService.GetById(cartId);
        cart.Status = (short)CartStatus.Process;
        await _cartService.Update(cart);

        var dto = _mapper.Map<OrderResponseDto>(entity);
        var response = new ApiResponse<OrderResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPost]
    [Route("{orderid:int}/FinishOrder/{cartId:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<OrderResponseDto>))]
    public async Task<IActionResult> FinishOrder([FromRoute] int orderid, [FromRoute] int cartId)
    {
        Expression<Func<Order, bool>> filter = x => x.Id == orderid;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(orderid);
        entity.Status = (short)OrderStatus.Finished;
        await _service.Update(entity);

        var cart = await _cartService.GetById(cartId);
        cart.Status = (short)CartStatus.Finished;
        cart.LastModifiedBy = _tokenHelper.GetUserName();
        cart.LastModifiedDate = DateTime.Now;
        await _cartService.Update(cart);

        var dto = _mapper.Map<OrderResponseDto>(entity);
        var response = new ApiResponse<OrderResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPut]
    [Route("{orderDrinkId:int}/StatusOrderDrink")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<OrderDrinkResponseDto>))]
    public async Task<IActionResult> ChangeStatusDrink([FromRoute] int orderDrinkId, [FromBody] short status)
    {
        Expression<Func<OrderDrink, bool>> filter = x => x.Id == orderDrinkId;
        var existEntity = await _drinkService.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _drinkService.GetById(orderDrinkId);
        entity.Status = status;
        await _drinkService.Update(entity);

        var dto = _mapper.Map<OrderDrinkResponseDto>(entity);
        var response = new ApiResponse<OrderDrinkResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPut]
    [Route("{orderFoodId:int}/StatusOrderFood")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<OrderFoodResponseDto>))]
    public async Task<IActionResult> ChangeStatusFood([FromRoute] int orderFoodId, [FromBody] short status)
    {
        Expression<Func<OrderFood, bool>> filter = x => x.Id == orderFoodId;
        var existEntity = await _foodService.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _foodService.GetById(orderFoodId);
        entity.Status = status;
        await _foodService.Update(entity);

        var dto = _mapper.Map<OrderFoodResponseDto>(entity);
        var response = new ApiResponse<OrderFoodResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpDelete]
    [Route("{orderid:int}/CancelOrder/{cartId:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<OrderResponseDto>))]
    public async Task<IActionResult> CancelOrder([FromRoute] int orderid, [FromRoute] int cartId)
    {
        Expression<Func<Order, bool>> filter = x => x.Id == orderid;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(orderid);
        entity.Status = (short)OrderStatus.Canceled;
        await _service.Update(entity);

        var cart = await _cartService.GetById(cartId);
        cart.Status = (short)CartStatus.Canceled;
        cart.LastModifiedBy = _tokenHelper.GetUserName();
        cart.LastModifiedDate = DateTime.Now;
        await _cartService.Update(cart);

        var dto = _mapper.Map<OrderResponseDto>(entity);
        var response = new ApiResponse<OrderResponseDto>(data: dto);
        return Ok(response);
    }
}