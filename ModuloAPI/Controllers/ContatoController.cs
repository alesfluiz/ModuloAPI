using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuloAPI.Context;
using ModuloAPI.Entities;
using System.Linq;

namespace ModuloAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private readonly AgendaContext _context;

        public ContatoController(AgendaContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contato contato)
        {
            await _context.AddAsync(contato);
            await _context.SaveChangesAsync();
            return Ok(contato);
        }


        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var contato = await _context.Contatos.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (contato == null)
                return NotFound();
            return Ok(contato);
        }

        [HttpGet("{nome}")]
        public async Task<IActionResult> ObterPorNome(string nome)
        {
            var contato = await _context.Contatos.Where(x => x.Nome == nome).ToArrayAsync();

            if (contato == null)
                return NotFound();
            return Ok(contato);

        }
        [HttpDelete("{idRemove}")]
        public async Task<IActionResult> Delete(int idRemove)
        {
            var contato = await _context.Contatos.Where(x => x.Id == idRemove).FirstOrDefaultAsync();
            if (contato == null)
            {
                return NotFound("Id não encontrado!");
            }
            else
            {
                _context.Remove(contato);
                await _context.SaveChangesAsync();
                return Ok(contato);
            }

        }

    }
}
