using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace senai.spacekids.domain.Entities
{
    public class Fase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FaseId {get;set;}
        
        [Required]
        [StringLength(100)]
        public string nome {get;set;}
    }
}