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
using Res.Common.QueryFilters;

namespace Res.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class JobController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICatalogBaseService<Job> _service;
    private readonly TokenHelper _tokenHelper;

    public JobController(IMapper mapper, ICatalogBaseService<Job> service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BaseCatalogResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<BaseCatalogResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<BaseCatalogResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] BaseCatalogQueryFilter filter)
    {
        var filters = _mapper.Map<Job>(filter);
        var entities = await _service.GetPaged(filters);
        var dto = _mapper.Map<IEnumerable<BaseCatalogResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<BaseCatalogResponseDto>>(data: dto, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<BaseCatalogResponseDto>))]
    public async Task<IActionResult> Create([FromBody] BaseCatalogCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Job>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();
        await _service.Create(entity);
        var dto = _mapper.Map<BaseCatalogResponseDto>(entity);
        var response = new ApiResponse<BaseCatalogResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BaseCatalogUpdateRequestDto requestDto)
    {
        Expression<Func<Job, bool>> filter = x => x.Id == id;
        var existRol = await _service.Exist(filter);
        if (!existRol)
            return BadRequest("No se encontró ningun Rol con ese id");

        var newEntity = _mapper.Map<Job>(requestDto);
        newEntity.Id = id;
        newEntity.IsDeleted = false;
        newEntity.LastModifiedDate = DateTime.Now;
        newEntity.LastModifiedBy = _tokenHelper.GetUserName();
        // newEntity.LastModifiedBy = _tokenHelper.GetUserName();
        await _service.Update(newEntity);
        return Ok(requestDto);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        Expression<Func<Job, bool>> filter = x => x.Id == id;
        var existRol = await _service.Exist(filter);
        if (!existRol)
            return BadRequest("No se encontró ningun Rol con ese id");

        var entity = await _service.GetById(id);
        entity.IsDeleted = true;
        entity.LastModifiedDate = DateTime.Now;
        entity.LastModifiedBy = _tokenHelper.GetUserName();
        entity.Id = id;
        await _service.Update(entity);
        return Ok(true);
    }
}