using Microsoft.AspNetCore.Mvc;
using PersonaPoliticas.Services;
using PersonaPoliticas.Models;
using System.Net;

namespace PersonaPoliticas.Controllers
{
    [Route("api/[controller]")]
    public class PersonaPadreController : ControllerBase
    {

        IPersonaPadreService personaPadreService;

        public PersonaPadreController(IPersonaPadreService service)
        {
            personaPadreService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(personaPadreService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonaPadre padre)
        {

            var nuevoPadre = await personaPadreService.Save(padre);

            if (nuevoPadre != null)
            {
                return Ok(nuevoPadre); // Retorna el nuevo registro agregado
            }
            else
            {
                // Maneja el error si es necesario
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PersonaPadre padre)
        {
            var padreUpdate = personaPadreService.Update(id, padre);


            if (padreUpdate != null)
            {
                return Ok(padreUpdate); // Retorna el nuevo registro agregado
            }
            else
            {
                // Maneja el error si es necesario
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            personaPadreService.Delete(id);
            return Ok();
        }

    }
}
