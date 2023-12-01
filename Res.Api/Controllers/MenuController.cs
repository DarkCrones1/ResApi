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
public class MenuController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMenuService _service;
    private readonly TokenHelper _tokenHelper;
    private readonly IDrinkService _drinkService;
    private readonly IFoodService _foodService;

    public MenuController(IMapper mapper, IMenuService service, TokenHelper tokenHelper, IDrinkService drinkService, IFoodService foodService)
    {
        this._mapper = mapper;
        this._service = service;
        this._tokenHelper = tokenHelper;
        this._drinkService = drinkService;
        this._foodService = foodService;
    }

    [HttpGet]
    [Route("")]
    [AllowAnonymous]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<MenuDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<MenuDetailResponseDto>>))]
    [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<IEnumerable<MenuDetailResponseDto>>))]
    public async Task<IActionResult> Get([FromQuery] MenuQueryFilter filter)
    {
        var entities = await _service.GetPaged(filter);
        var dtos = _mapper.Map<IEnumerable<MenuDetailResponseDto>>(entities);
        var metaDataResponse = new MetaDataResponse(
            entities.TotalCount,
            entities.CurrentPage,
            entities.PageSize
        );
        var response = new ApiResponse<IEnumerable<MenuDetailResponseDto>>(data: dtos, meta: metaDataResponse);
        return Ok(response);
    }

    [HttpPost]
    [Route("")]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<MenuDetailResponseDto>))]
    public async Task<IActionResult> Create([FromBody] MenuCreateRequestDto requestDto)
    {
        var entity = _mapper.Map<Menu>(requestDto);
        entity.CreatedBy = _tokenHelper.GetUserName();
        
        if (requestDto.DrinkIds != null)
        {
            foreach (var item in requestDto.DrinkIds)
            {
                var drink = await _drinkService.GetById(item);
                if (drink.Id > 0)
                    entity.Drink.Add(drink);
            }
        }

        if (requestDto.FoodIds != null)
        {
            foreach (var item in requestDto.FoodIds)
            {
                var food = await _foodService.GetById(item);
                if (food.Id > 0)
                    entity.Food.Add(food);
            }
        }

        await _service.Create(entity);
        var dto = _mapper.Map<MenuDetailResponseDto>(entity);
        var response = new ApiResponse<MenuDetailResponseDto>(data: dto);
        return Ok(response);
    }
}