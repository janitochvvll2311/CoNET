using CoNET.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CoNET.Controllers;

public class CrudController<T, Q> : ControllerBase
    where T : class, IEntity, IUpdatable<T>, IJsonObject, new()
{

    public IRepository<T> Repository { get; }
    public List<string> Errors { get; }

    public CrudController(IRepository<T> repository)
    {
        Repository = repository;
        Errors = new List<string>();
    }

    [HttpGet]
    public virtual IActionResult Get([FromQuery] Q query)
    {
        var entities = Repository.GetAll();
        return Ok(entities.Select(x => x.ToJson()));
    }

    [HttpGet("{id}")]
    public virtual IActionResult Get([FromRoute] int id, [FromQuery] Q query)
    {
        var entity = Repository.GetById(id);
        if (entity == null)
        {
            return NotFound();
        }
        return Ok(entity.ToJson());
    }

    [HttpPost]
    public virtual IActionResult Insert([FromBody] T entity, [FromQuery] Q query)
    {
        entity.Id = 0;
        if (!Repository.ValidateInsert(entity, Errors))
        {
            return BadRequest(Errors);
        }
        Repository.Insert(entity);
        return Ok(entity.ToJson());
    }

    [HttpPut("{id}")]
    public virtual IActionResult Insert([FromRoute] int id, [FromBody] T entity, [FromQuery] Q query)
    {
        var original = Repository.GetById(id);
        if (original == null)
        {
            return NotFound();
        }
        entity.Id = id;
        if (!Repository.ValidateUpdate(entity, Errors))
        {
            return BadRequest(Errors);
        }
        original.Update(entity);
        Repository.Insert(original);
        return Ok(original.ToJson());
    }

    [HttpDelete("{id}")]
    public virtual IActionResult Insert([FromRoute] int id, [FromQuery] Q query)
    {
        var entity = Repository.GetById(id);
        if (entity == null)
        {
            return NotFound();
        }
        Repository.Delete(entity);
        return Ok(entity.ToJson());
    }

}