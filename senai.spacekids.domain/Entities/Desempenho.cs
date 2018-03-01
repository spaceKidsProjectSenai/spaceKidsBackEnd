using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace senai.spacekids.domain.Entities
{
    public class Desempenho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int desempenhoId { get; set; }
        [Required]
        public bool acertou{get;set;}
        [Required]
        public DateTime horaInicial { get; set; }
        [Required]
        public DateTime horaFinal { get; set; }

        [ForeignKey("criancaId")]
        public Crianca Crianca { get; set; }
        public int criancaId { get; set; }

        [ForeignKey("faseId")]
        public Fase Fase{get;set;}
        public int faseId { get; set; }

    }
}