using Agendamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Agendamento
{
    public class AgendamentoContext: DbContext
    {
        public DbSet<Paciente> Paciente { get; set; }
        public DbSet<Medico> Medico { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Models.Agendamento> Agendamento { get; set; }

        public AgendamentoContext(DbContextOptions<AgendamentoContext> options) :
            base(options)
        {
        }
    }
}