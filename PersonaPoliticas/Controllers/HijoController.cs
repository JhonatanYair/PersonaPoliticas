using Microsoft.AspNetCore.Mvc;
using PersonaPoliticas.Models;
using PersonaPoliticas.Services;

namespace PersonaPoliticas.Controllers
{
    [Route("api/[controller]")]
    public class HijoController : Controller
    {
        IHijoService hijoService;

        public HijoController(IHijoService service)
        {
            hijoService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(hijoService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Hijo hijo)
        {
            await hijoService.Save(hijo);
            return Ok(); // Retorna el nuevo registro agregado
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Hijo hijo)
        {
            await hijoService.Update(id, hijo);
            return Ok(); // Retorna el nuevo registro agregado
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            hijoService.Delete(id);
            return Ok();
        }
    }
}
