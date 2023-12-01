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
public class BoxCashController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBoxCashService _service;
    private readonly TokenHelper _tokenHelper;

    public BoxCashController(IMapper mapper, IBoxCashService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BoxCashResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<BoxCashResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<BoxCashResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] BoxCashQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<BoxCashResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<BoxCashResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<BoxCashResponseDto>))]
    public async Task<IActionResult> Create([FromBody] BoxCashCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<BoxCash>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();
            await _service.Create(entity);
            var dto = _mapper.Map<BoxCashResponseDto>(entity);
            var response = new ApiResponse<BoxCashResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BoxCashUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<BoxCash, bool>> filter = x => x.Id == id;
            var existEntity = await _service.Exist(filter);

            if (!existEntity)
                return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

            var newEntity = _mapper.Map<BoxCash>(requestDto);
            newEntity.IsDeleted = false;
            newEntity.Id = id;
            newEntity.LastModifiedDate = DateTime.Now;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();

            await _service.Update(newEntity);
            return Ok(requestDto);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Expression<Func<BoxCash, bool>> filter = x => x.Id == id;
            var existEntity = await _service.Exist(filter);

            if (!existEntity)
                return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

            var entity = await _service.GetById(id);
            entity.IsDeleted = true;
            entity.LastModifiedDate = DateTime.Now;
            entity.LastModifiedBy = _tokenHelper.GetUserName();
            await _service.Update(entity);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }
}