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
public class UserAccountController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IUserAccountService _service;
    private readonly ICrudService<Rol> _rolService;
    private readonly ICrudService<BranchStore> _branchService;
    private readonly TokenHelper _tokenHelper;

    public UserAccountController(IMapper mapper, IUserAccountService service, ICrudService<Rol> rolService, ICrudService<BranchStore> branchService, TokenHelper tokenHelper)
    {
        this._rolService = rolService;
        this._branchService = branchService;
        this._tokenHelper = tokenHelper;
        this._mapper = mapper;
        this._service = service;
    }

    
}