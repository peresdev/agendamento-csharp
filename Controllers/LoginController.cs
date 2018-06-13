using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Agendamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendamento.Controllers
{
    public class LoginController : Controller
    {
        private readonly AgendamentoContext _context;

        public LoginController(AgendamentoContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logar([Bind("Login, Senha")] Usuario usuario)
        {
            var usuario_valido = await _context.Usuario.SingleOrDefaultAsync(l => l.Login == usuario.Login && l.Senha == usuario.Senha);
            if (usuario_valido == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Index", "Home");
        }

    }
}