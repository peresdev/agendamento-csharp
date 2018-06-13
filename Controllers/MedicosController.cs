using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Agendamento.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Agendamento.Controllers
{
    public class MedicosController : Controller
    {

        private readonly AgendamentoContext _context;

        public MedicosController(AgendamentoContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Medico.ToListAsync());
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar([Bind("MedicoID, Nome")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medico.SingleOrDefaultAsync(m => m.MedicoID == id);
            if (medico == null)
            {
                return NotFound();
            }
            return View(medico);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int MedicoID, [Bind("MedicoID, Nome")] Medico medico)
        {
            if (MedicoID != medico.MedicoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExisteMedico(medico.MedicoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(medico);
        }

        public async Task<IActionResult> Remover(int id)
        {
            if (ExisteMedico(id))
            {
                var medico = await _context.Medico.SingleOrDefaultAsync(m => m.MedicoID == id);
                _context.Medico.Remove(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        private bool ExisteMedico(int id)
        {
            return _context.Medico.Any(e => e.MedicoID == id);
        }

        [HttpGet("api/medicos")]
        public async Task<JsonResult> GetMedicosAsync()
        {
            return new JsonResult(new List<object>()
            {
                await _context.Medico.ToListAsync()
            });
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
