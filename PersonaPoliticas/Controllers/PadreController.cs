using Microsoft.AspNetCore.Mvc;
using PersonaPoliticas.Services;
using PersonaPoliticas.Models;
using System.Net;

namespace PersonaPoliticas.Controllers
{
    [Route("api/[controller]")]
    public class PadreController : ControllerBase
    {

        IPadreService padreService;

        public PadreController(IPadreService service)
        {
            padreService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(padreService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Padre padre)
        {
             await padreService.Save(padre);
            return Ok(); // Retorna el nuevo registro agregado
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Padre padre)
        {
                await padreService.Update(id, padre);
                return Ok(); // Retorna el nuevo registro agregado
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            padreService.Delete(id);
            return Ok();
        }

    }
}
