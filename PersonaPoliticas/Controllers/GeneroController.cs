using Microsoft.AspNetCore.Mvc;
using PersonaPoliticas.Models;
using PersonaPoliticas.Services;
using System.Net;

namespace PersonaPoliticas.Controllers
{
    [Route("api/[controller]")]
    public class GeneroController : ControllerBase
    {
        IGeneroService generoService;

        public GeneroController(IGeneroService service)
        {
            generoService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(generoService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Genero genero)
        {

             await generoService.Save(genero);           
             return Ok(); // Retorna el nuevo registro agregado           
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Genero genero)
        {
            var generoUpdate = generoService.Update(id, genero);


            if (generoUpdate != null)
            {
                return Ok(generoUpdate); // Retorna el nuevo registro agregado
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
            generoService.Delete(id);
            return Ok();
        }
    }
}
