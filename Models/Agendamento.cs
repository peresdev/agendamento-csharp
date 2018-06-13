using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Agendamento.Models
{
    [Table("Agendamentos")]
    public class Agendamento
    {
        [Key]
        public int AgendamentoID { get; set; }
        [Required]
        public DateTime Data { get; set; }
        public int? PacienteID { get; set; }
		public int? MedicoID { get; set; }
        [NotMapped]
        public string NomePaciente { get; set; }
		[NotMapped]
        public string NomeMedico { get; set; }
    }
}