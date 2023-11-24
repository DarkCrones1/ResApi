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
using Res.Common.Entities;

namespace Res.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CrudController<T> : ControllerBase where T : BaseEntity
{
    private readonly ICrudService<T> _service;
    private readonly IMapper _mapper;

    public CrudController(ICrudService<T> service, IMapper mapper)
    {
        this._service = service;
        this._mapper = mapper;
    }

    [HttpGet]
    public virtual async Task<IActionResult> List<D>()
    {
        var entities = await _service.GetAll();
        return Ok(entities);
    }

    [HttpGet("{id:int}")]
    public virtual async Task<IActionResult> Detail(int id)
    {
        var entity = await _service.GetById(id);

        if (entity == null)
            return NotFound();

        return Ok(entity);
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create(T entity)
    {
        await _service.Create(entity);

        return CreatedAtAction("Detail", new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(int id, T entity)
    {
        if (id != entity.Id)
            return BadRequest();

        if (!await EntityExists(id))
            return NotFound();

        await _service.Update(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _service.GetById(id);

        if (entity == null)
            return NotFound();

        await _service.Delete(id);

        return NoContent();
    }

    private Task<bool> EntityExists(int id)
    {
        Expression<Func<T, bool>> filter = x => x.Id == id;
        return _service.Exist(filter);
    }
}