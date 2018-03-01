using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace senai.spacekids.domain.Entities
{
    public class Pai
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int paiId {get;set;}
        [Required]
        [StringLength(100)]
        public string nome {get;set;}

        [ForeignKey("loginId")]
        public Login Login{get;set;}
        public int loginId{get;set;}

        public ICollection<Crianca> Criancas{get;set;}
    }
}