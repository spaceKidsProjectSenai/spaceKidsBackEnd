using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
namespace senai.spacekids.domain.Entities
{
    public class Crianca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int criancaId {get;set;}

        [Required]
        [StringLength(100)]
        public string nome {get;set;}

        [Required]
        public int idade {get;set;}
        
        [Required]
        public int sexo;
        public string foto {get;set;}

        [ForeignKey("paiId")]
        public Pai pai {get;set;}
        public int paiId{get;set;}


        public ICollection<Fase> Fases{get;set;}
    }
}