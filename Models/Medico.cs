using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento.Models
{
    [Table("Medicos")]
    public class Medico
    {
        public int MedicoID { get; set; }
        [Required]
        public string Nome { get; set; }
        public ICollection<Agendamento> Agendamentos { get; set; }
    }
}