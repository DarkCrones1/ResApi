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
public class CategoryController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICategoryService _service;
    private readonly TokenHelper _tokenHelper;

    public CategoryController(IMapper mapper, ICategoryService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] CategoryQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<CategoryResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );

        var response = new ApiResponse<IEnumerable<CategoryResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<CategoryResponseDto>>))]
    public async Task<IActionResult> GetAll([FromRoute] int id)
    {
        Expression<Func<Category, bool>> filter = x => x.Id == id;
        var existCategory = await _service.Exist(filter);

        if (!existCategory)
            return BadRequest("No existen coincidencias");

        var entity = await _service.GetById(id);
        var dto = _mapper.Map<CategoryResponseDto>(entity);
        var response = new ApiResponse<CategoryResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CategoryResponseDto>))]
    public async Task<IActionResult> Create([FromBody] CategoryCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Category>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();
            await _service.Create(entity);
            var dto = _mapper.Map<CategoryResponseDto>(entity);
            var response = new ApiResponse<CategoryResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<CategoryResponseDto>))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] CategoryUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Category, bool>> filter = x => x.Id == id;
            var existCategory = await _service.Exist(filter);

            if (!existCategory)
                return BadRequest("No se encontró el elemento que desea modificar");

            var newEntity = _mapper.Map<Category>(requestDto);
            newEntity.IsDeleted = false;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();
            newEntity.LastModifiedDate = DateTime.Now;
            newEntity.Id = id;
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
    [Authorize]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        try
        {
            Expression<Func<Category, bool>> filter = x => x.Id == id;
            var existCategory = await _service.Exist(filter);

            if (!existCategory)
                return BadRequest("No se encontró el elemento que desea modificar");

            var oldEntity = await _service.GetById(id);
            oldEntity.IsDeleted = true;
            oldEntity.LastModifiedBy = _tokenHelper.GetUserName();
            oldEntity.LastModifiedDate = DateTime.Now;
            await _service.Update(oldEntity);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

}