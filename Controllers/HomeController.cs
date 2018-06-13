using Microsoft.AspNetCore.Mvc;
using Agendamento.Models;
using System.Linq;

namespace Agendamento.Controllers
{
    public class HomeController : Controller
    {
        private readonly AgendamentoContext _context;

        public HomeController(AgendamentoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var pacientes_cadastrados = _context.Paciente.Count();
            var medicos_cadastrados = _context.Medico.Count();
            var agendamentos_realizados = _context.Agendamento.Count();

            ViewBag.PacientesCadastrados = pacientes_cadastrados;
            ViewBag.MedicosCadastrados = medicos_cadastrados;
            ViewBag.AgendamentosRealizados = agendamentos_realizados;

            return View();
        }
    }
}
