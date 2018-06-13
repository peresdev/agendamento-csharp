using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Agendamento.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Agendamento.Controllers
{
    public class AgendamentosController : Controller
    {

        private readonly AgendamentoContext _context;

        public AgendamentosController(AgendamentoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var agendamentos_query = from agendamentos in _context.Agendamento
                        join pacientes in _context.Paciente on agendamentos.PacienteID equals pacientes.PacienteID
                        join medicos in _context.Medico on agendamentos.MedicoID equals medicos.MedicoID
                        select new Models.Agendamento
                        {
                            AgendamentoID = agendamentos.AgendamentoID,
                            Data = agendamentos.Data,
                            NomePaciente = pacientes.Nome,
                            NomeMedico = medicos.Nome
                        };

            return View(agendamentos_query);

        }

        public IActionResult Adicionar()
        {
            PopularPacientesDropDownList();
            PopularMedicosDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar([Bind("AgendamentoID, Data, PacienteID, MedicoID")] Models.Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(agendamento);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento.SingleOrDefaultAsync(m => m.AgendamentoID == id);
            if (agendamento == null)
            {
                return NotFound();
            }
            PopularPacientesDropDownList(agendamento);
            PopularMedicosDropDownList(agendamento);
            return View(agendamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int AgendamentoID, [Bind("AgendamentoID, Data, PacienteID, MedicoID")] Models.Agendamento agendamento)
        {
            if (AgendamentoID != agendamento.AgendamentoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExisteAgendamento(agendamento.AgendamentoID))
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
            return View(agendamento);
        }

        public async Task<IActionResult> Remover(int id)
        {
            if (ExisteAgendamento(id))
            {
                var agendamento = await _context.Agendamento.SingleOrDefaultAsync(m => m.AgendamentoID == id);
                _context.Agendamento.Remove(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        private bool ExisteAgendamento(int id)
        {
            return _context.Agendamento.Any(e => e.AgendamentoID == id);
        }

        private void PopularPacientesDropDownList(object selectedPaciente = null)
        {
            var pacientesQuery = from a in _context.Paciente
                              orderby a.Nome
                              select a;
            ViewBag.PacienteID = new SelectList(pacientesQuery.AsNoTracking(), "PacienteID", "Nome", selectedPaciente);
        }

        private void PopularMedicosDropDownList(object selectedMedico = null)
        {
            var medicosQuery = from a in _context.Medico
                              orderby a.Nome
                              select a;
            ViewBag.MedicoID = new SelectList(medicosQuery.AsNoTracking(), "MedicoID", "Nome", selectedMedico);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
