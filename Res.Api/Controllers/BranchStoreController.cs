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

    [HttpPost]
    [Authorize]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<BranchStoreResponseDto>))]
    public async Task<IActionResult> Create([FromBody] BranchStoreCreateRequestDto requestDto)
    {
        try
        {
            var entity = _mapper.Map<BranchStore>(requestDto, opts => opts.Items["CreatedUser"] = _tokenHelper.GetUserName());
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
}