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
public class TicketController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICrudService<Ticket> _service;
    private readonly TokenHelper _tokenHelper;
    private readonly ICrudService<Cart> _cartService;

    public TicketController(IMapper mapper, ICrudService<Ticket> service, TokenHelper tokenHelper, ICrudService<Cart> cartService)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._cartService = cartService;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<TicketResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<TicketResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<TicketResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] TicketQueryFilter filter)
    {
        var filters = _mapper.Map<Ticket>(filter);
        var entities = await _service.GetPaged(filters);
        var dtos = _mapper.Map<IEnumerable<TicketResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );

        var response = new ApiResponse<IEnumerable<TicketResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<TicketDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<TicketDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<TicketDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Expression<Func<Ticket, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);

        var dto = _mapper.Map<TicketDetailResponseDto>(entity);
        var response = new ApiResponse<TicketDetailResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TicketResponseDto>))]
    public async Task<IActionResult> Create([FromBody] TicketCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Ticket>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();
        await _service.Create(entity);

        var cart = await _cartService.GetById(entity.CartId);
        cart.Status = (short)CartStatus.Pendding;

        var dto = _mapper.Map<TicketResponseDto>(entity);
        var response = new ApiResponse<TicketResponseDto>(data: dto);
        return Ok(response);
    }

    // [HttpPost]
    // [Route("{id:int}/PayTicket")]
    // [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TicketResponseDto>))]
    // public async Task<IActionResult> payTicket([FromRoute] int id)
    // {
    //     Expression<Func<Ticket, bool>> filter = x => x.Id == id;
    //     var existEntity = await _service.Exist(filter);

    //     if (!existEntity)
    //         return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

    //     var entity = await _service.GetById(id);
    //     entity.Status = (short)TicketStatus.Payment;
    //     entity.CloseTicket = DateTime.Now;
    //     entity.LastModifiedBy = _tokenHelper.GetUserName();
    //     entity.LastModifiedDate = DateTime.Now;
        
    //     await _service.Update(entity);
        
    //     var dto = _mapper.Map<TicketResponseDto>(entity);
    //     var response = new ApiResponse<TicketResponseDto>(data: dto);
    //     return Ok(response);
    // }

    [HttpDelete]
    [Route("{id:int}/CancelTicket")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<TicketResponseDto>))]
    public async Task<IActionResult> CancelTicket([FromRoute] int id)
    {
        Expression<Func<Ticket, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);
        entity.Status = (short)TicketStatus.Canceled;
        entity.LastModifiedBy = _tokenHelper.GetUserName();
        entity.CloseTicket = DateTime.Now;
        entity.LastModifiedDate = DateTime.Now;
        
        await _service.Update(entity);
        
        var dto = _mapper.Map<TicketResponseDto>(entity);
        var response = new ApiResponse<TicketResponseDto>(data: dto);
        return Ok(response);
    }
}