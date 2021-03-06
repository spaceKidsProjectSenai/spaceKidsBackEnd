using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace senai.spacekids.domain.Entities
{
    public class Login 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoginId {get;set;}
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [DataType(DataType.EmailAddress)]
        public string email {get;set;}

        [Required]
        [StringLength(12, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string senha {get;set;}

        [Required]
        [StringLength(100)]
        public string nome {get;set;}

        [StringLength(100)]
        public string Permissao { get; set; }

        public ICollection<Crianca> Criancas{get;set;}
    }
}