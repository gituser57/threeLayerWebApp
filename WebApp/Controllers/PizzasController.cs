using Business.Models;
using Business.Interfaces;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Presentation.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
   
    public class PizzasController : ControllerBase
    {
        private readonly IPizzaService _pizzasService;

        public PizzasController(IPizzaService pizzasService)
        {
            _pizzasService = pizzasService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PizzaModel>> Get()
        {
            var pizzas = _pizzasService.GetAll();
            return pizzas.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<PizzaModel>>> GetById(int id)
        {
            var pizza = await _pizzasService.GetByIdAsync(id);

            if (pizza == null)
            {
                return NotFound();
            }

            return Ok(pizza);
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] PizzaModel pizzaModel)
        {
            try
            {
                await _pizzasService.AddAsync(pizzaModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return CreatedAtAction(nameof(Add), new { pizzaModel.Id }, pizzaModel);
        }

        [HttpPut]
        public async Task<ActionResult> Update(PizzaModel bookModel)
        {
            try
            {
                await _pizzasService.UpdateAsync(bookModel);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _pizzasService.DeleteByIdAsync(id);

            return Ok();
        }

        [HttpGet("price")]
        public ActionResult<IEnumerable<PizzaModel>> GeCertainPricePizza(decimal minPrice, decimal maxPrice)
        {
            var pizzas = _pizzasService.GetPizzaByPrice(minPrice, maxPrice);

            if (pizzas == null)
                return NotFound();

            return Ok(pizzas);
        }
    }
}
