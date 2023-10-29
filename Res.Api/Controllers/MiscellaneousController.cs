using System.Net;

using Microsoft.AspNetCore.Mvc;

using Res.API.Responses;
using Res.Common.Dtos.Response;
using Res.Domain.Interfaces.Services;

namespace gem.api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MiscellaneousController : ControllerBase
{
    private readonly IMiscellaneousService _service;

    public MiscellaneousController(IMiscellaneousService service)
    {
        this._service = service;
    }

    [HttpGet]
    [Route("CartStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetCartStatus()
    {
        var lstItem = await _service.GetCartStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("CustomerTypeStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetCustomerTypeStatus()
    {
        var lstItem = await _service.GetCustomerTypeStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("Gender")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetGender()
    {
        var lstItem = await _service.GetGender();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("OrderDrinkStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetOrdenDrinkStatus()
    {
        var lstItem = await _service.GetOrderDrinkStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("OrderFoodStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetOrderFoodStatus()
    {
        var lstItem = await _service.GetOrderFoodStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("OrderStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetOrderStatus()
    {
        var lstItem = await _service.GetOrderStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("PaymentStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetPaymentStatus()
    {
        var lstItem = await _service.GetPaymentStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("ProductType")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetProductType()
    {
        var lstItem = await _service.GetProductType();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("ReservationStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetReservationStatus()
    {
        var lstItem = await _service.GetReservationStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("TicketStatus")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetTicketStatus()
    {
        var lstItem = await _service.GetTicketStatus();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }

    [HttpGet]
    [Route("UserAccountType")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<EnumValueResponseDto>>))]
    [ResponseCache(Duration = 300)]
    public async Task<IActionResult> GetUserAccountType()
    {
        var lstItem = await _service.GetUserAccountType();
        var response = new ApiResponse<IEnumerable<EnumValueResponseDto>>(lstItem);
        return Ok(response);
    }
}