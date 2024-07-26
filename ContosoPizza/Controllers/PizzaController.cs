using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContosoPizza.Controllers;

[ApiController]
[Route("[controller]")]
[ApiExplorerSettings(GroupName = "v2")]
[Authorize]
public class PizzaController : ControllerBase
{
    private readonly IPizzaService _pizzaService;
    public PizzaController(IPizzaService pizzaService)
    {
        _pizzaService=pizzaService;
    }

    // GET all action
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pizza>>> GetAll() {
       var pizzas = await  _pizzaService.GetAllAsync();
       return Ok(pizzas);
    }
   

    // GET by Id action
    [HttpGet("{id}")]
    public async Task<ActionResult<MyEntity>> Get(int id)
    {
        var entity = await _pizzaService.GetByIdAsync(id);
        if (entity == null)
        {
        return NotFound();
        }
        return Ok(entity);
    }




    // POST action

    // PUT action

    // DELETE action
}