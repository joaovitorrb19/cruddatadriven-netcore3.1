using System.ComponentModel.DataAnnotations;

namespace appteste{
    public class Usuario{
        [Key]
        public int Id {get;set;}

        [Required]
        public string usuario {get;set;}

        [Required]
        public string password {get;set;}

        public string role {get;set;}    
    }
}