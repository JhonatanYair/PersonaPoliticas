using Microsoft.AspNetCore.Mvc;
using PersonaPoliticas.Models;
using PersonaPoliticas.Services;
using System.Net;

namespace PersonaPoliticas.Controllers
{
    [Route("api/[controller]")]
    public class PersonaHijoController : Controller
    {
        IPersonaHijoService personaHijoService;

        public PersonaHijoController(IPersonaHijoService service)
        {
            personaHijoService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(personaHijoService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonaHijo personaHijo)
        {

            var nuevoHijo = await personaHijoService.Save(personaHijo);

            if (nuevoHijo != null)
            {
                return Ok(nuevoHijo); // Retorna el nuevo registro agregado
            }
            else
            {
                // Maneja el error si es necesario
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PersonaHijo personaHijo)
        {
            var hijoUpdate = personaHijoService.Update(id, personaHijo);


            if (hijoUpdate != null)
            {
                return Ok(hijoUpdate); // Retorna el nuevo registro agregado
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
            personaHijoService.Delete(id);
            return Ok();
        }
    }
}
