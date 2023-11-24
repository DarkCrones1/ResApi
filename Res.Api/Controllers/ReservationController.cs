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
}