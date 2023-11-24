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
public class PaymentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICrudService<Payment> _service;
    private readonly TokenHelper _tokenHelper;
    private readonly ICrudService<Ticket> _ticketService;
    private readonly ICrudService<Cart> _cartService;

    public PaymentController(IMapper mapper, ICrudService<Payment> service, TokenHelper tokenHelper, ICrudService<Ticket> ticketService, ICrudService<Cart> cartService)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._ticketService = ticketService;
        this._cartService = cartService;
    }

    [HttpPost]
    [Route("Ticket")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TicketResponseDto>))]
    public async Task<IActionResult> Create([FromBody] PaymentCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Payment>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();
        await _service.Create(entity);

        var ticket = await _ticketService.GetById(entity.TicketId);
        ticket.Status = (short)TicketStatus.Payment;
        await _ticketService.Update(ticket);

        var cart = await _cartService.GetById(ticket.CartId);
        cart.Status = (short)CartStatus.Closed;
        cart.LastModifiedBy = _tokenHelper.GetUserName();
        cart.LastModifiedDate = DateTime.Now;
        await _cartService.Update(cart);

        var dto = _mapper.Map<TicketResponseDto>(entity);
        var response = new ApiResponse<TicketResponseDto>(data: dto);
        return Ok(response);
    }
}