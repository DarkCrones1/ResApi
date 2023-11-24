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
public class DrinkController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICatalogBaseService<Drink> _service;
    private readonly TokenHelper _tokenHelper;
    private readonly ICategoryService _categoryService;

    public DrinkController(IMapper mapper, ICatalogBaseService<Drink> service, TokenHelper tokenHelper, ICategoryService categoryService)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._categoryService = categoryService;
    }

    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<DrinkResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<DrinkResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<DrinkResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] DrinkQueryFilter filter)
    {
        var filters = _mapper.Map<Drink>(filter);
        var entities = await _service.GetPaged(filters);

        var dtos = _mapper.Map<IEnumerable<DrinkResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<DrinkResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<DrinkDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<DrinkDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<DrinkDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Expression<Func<Drink, bool>> filter = x => x.Id == id;
        var existDrink = await _service.Exist(filter);

        if (!existDrink)
            return BadRequest("No se encontró el elemento que dese modificar");

        var entity = await _service.GetById(id);

        var dto = _mapper.Map<DrinkDetailResponseDto>(entity);
        var response = new ApiResponse<DrinkDetailResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<DrinkResponseDto>))]
    public async Task<IActionResult> Create([FromBody] DrinkCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<Drink>(requestDto);
            entity.CreatedBy = _tokenHelper.GetUserName();
            if (requestDto.CategoryIds != null)
            {
                foreach (var item in requestDto.CategoryIds)
                {
                    var category = await _categoryService.GetById(item);
                    if (category.Id > 0)
                        entity.Category.Add(category);
                }
            }
            
            await _service.Create(entity);
            var dto = _mapper.Map<DrinkResponseDto>(entity);
            var response = new ApiResponse<DrinkResponseDto>(data: dto);
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
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<DrinkResponseDto>))]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] DrinkUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<Drink, bool>> filter = x => x.Id == id;
            var existDrink = await _service.Exist(filter);

            if (!existDrink)
                return BadRequest("No se encontró el elemento que dese modificar");

            var oldEntity = await _service.GetById(id);
            oldEntity.Category.Clear();
            var newEntity = _mapper.Map<Drink>(requestDto);
            newEntity.IsDeleted = false;
            newEntity.LastModifiedDate = DateTime.Now;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();
            newEntity.Id = id;
            if (requestDto.CategoryIds != null)
            {
                foreach (var item in requestDto.CategoryIds)
                {
                    var category = await _categoryService.GetById(item);
                    if (category.Id > 0)
                        oldEntity.Category.Add(category);
                }
            }
            await _service.Update(newEntity);
            var dto = _mapper.Map<DrinkResponseDto>(newEntity);
            var response = new ApiResponse<DrinkResponseDto>(data: dto);
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
        Expression<Func<Drink, bool>> filter = x => x.Id == id;
        var existDrink = await _service.Exist(filter);

        if (!existDrink)
            return BadRequest("No se encontró el elemento que dese modificar");

        var entity = await _service.GetById(id);
        entity.IsDeleted = true;
        entity.LastModifiedBy = _tokenHelper.GetUserName();
        entity.LastModifiedDate = DateTime.Now;
        entity.Id = id;

        await _service.Update(entity);
        return Ok(true);
    }
}