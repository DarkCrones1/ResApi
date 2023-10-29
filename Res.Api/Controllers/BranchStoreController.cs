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
public class BranchStoreController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IBranchStoreService _service;
    private readonly TokenHelper _tokenHelper;

    public BranchStoreController(IMapper mapper, IBranchStoreService service, TokenHelper tokenHelper)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
    }

    [HttpGet]
    [Route("")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BranchStoreResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<BranchStoreResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<BranchStoreResponseDto>>))]
    public async Task<IActionResult> GetAll([FromQuery] BranchStoreQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<BranchStoreResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );

        var response = new ApiResponse<IEnumerable<BranchStoreResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<BranchStoreDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<BranchStoreDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<BranchStoreDetailResponseDto>>))]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        Expression<Func<BranchStore, bool>> filter = x => x.Id == id;
        var existEntity = await _service.Exist(filter);

        if (!existEntity)
            return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");

        var entity = await _service.GetById(id);

        var dto = _mapper.Map<BranchStoreDetailResponseDto>(entity);
        var response = new ApiResponse<BranchStoreDetailResponseDto>(data: dto);
        return Ok(response);
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<BranchStoreResponseDto>))]
    public async Task<IActionResult> Create([FromBody] BranchStoreCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<BranchStore>(requestDto); //opts => opts.Items["CreatedUser"] = _tokenHelper.GetUserName());
            await _service.Create(entity);
            var dto = _mapper.Map<BranchStoreResponseDto>(entity);
            var response = new ApiResponse<BranchStoreResponseDto>(data: dto);
            return Ok(response);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }

    [HttpPut]
    [Authorize]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BranchStoreUpdateRequestDto requestDto)
    {
        try
        {
            Expression<Func<BranchStore, bool>> filter = x => x.Id == id;
            var existEntity = await _service.Exist(filter);
    
            if (!existEntity)
                return NotFound("No se encontró un elemento que cumpla con la información proporcionada, verifique su información porfavor....");
    
            var newEntity = _mapper.Map<BranchStore>(requestDto);
    
            newEntity.IsDeleted = false;
            newEntity.Id = id;
            newEntity.LastModifiedBy = _tokenHelper.GetUserName();
            newEntity.LastModifiedDate = DateTime.Now;
    
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
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var entity = await _service.GetById(id);
            entity.IsDeleted = true;
            entity.LastModifiedBy = _tokenHelper.GetUserName();
            entity.LastModifiedDate = DateTime.Now;
            await _service.Update(entity);
            return Ok(true);
        }
        catch (Exception ex)
        {

            throw new LogicBusinessException(ex);
        }
    }
}