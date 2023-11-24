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

namespace Res.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
[Authorize]
public class ReservationController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICrudService<Reservation> _service;
    private readonly TokenHelper _tokenHelper;

    public ReservationController(IMapper mapper, ICrudService<Reservation> service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<ReservationResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<ReservationResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<ReservationResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] ReservationQueryFilter filter)
    {
        var filters = _mapper.Map<Reservation>(filter);
        var entities = await _service.GetPaged(filters);
        var dtos = _mapper.Map<IEnumerable<ReservationResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        
        var response = new ApiResponse<IEnumerable<ReservationResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ReservationResponseDto>))]
    public async Task<IActionResult> Create([FromBody] ReservationCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Reservation>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();
        await _service.Create(entity);

        var dto = _mapper.Map<ReservationResponseDto>(entity);
        var response = new ApiResponse<ReservationResponseDto>(dto);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:int}/StatusReservation")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ReservationResponseDto>))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] ReservationUpdateRequestDto requestDto)
    {
        Expression<Func<Reservation, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);
        entity.ReservationTime = requestDto.ReservationTime;
        entity.LastModifiedBy = _tokenHelper.GetUserName();
        entity.LastModifiedDate = DateTime.Now;
        entity.IsDeleted = false;
        await _service.Update(entity);

        var dto = _mapper.Map<ReservationResponseDto>(entity);
        var response = new ApiResponse<ReservationResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:int}/StatusReservation")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<ReservationResponseDto>))]
    public async Task<IActionResult> ChangeStatus([FromRoute] int id, [FromBody] short status)
    {
        Expression<Func<Reservation, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);
        entity.Status = status;
        entity.LastModifiedBy = _tokenHelper.GetUserName();
        entity.LastModifiedDate = DateTime.Now;
        entity.IsDeleted = false;
        await _service.Update(entity);

        var dto = _mapper.Map<ReservationResponseDto>(entity);
        var response = new ApiResponse<ReservationResponseDto>(data: dto);
        return Ok(response);
    }
}