using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace senai.spacekids.domain.Entities
{
    public class Login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int loginId {get;set;}
        [Required]
        [StringLength(50, MinimumLength = 4)]
        [DataType(DataType.EmailAddress)]
        public string email {get;set;}
        [Required]
        [StringLength(12, MinimumLength = 4)]
        [DataType(DataType.Password)]
        public string senha {get;set;}
    }
}