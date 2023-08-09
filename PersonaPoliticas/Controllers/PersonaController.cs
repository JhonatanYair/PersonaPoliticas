using Microsoft.AspNetCore.Mvc;
using PersonaPoliticas.Models;
using PersonaPoliticas.Services;
using System.Net;

namespace PersonaPoliticas.Controllers
{
    [Route("api/[controller]")]
    public class PersonaController : Controller
    {
        IPersonaService personaService;

        public PersonaController(IPersonaService service)
        {
            personaService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(personaService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Persona persona)
        {
             await personaService.Save(persona);
             return Ok(); // Retorna el nuevo registro agregado
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Persona persona)
        {
                personaService.Update(id, persona);
                return Ok(); // Retorna el nuevo registro agregado            
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            personaService.Delete(id);
            return Ok();
        }
    }
}
