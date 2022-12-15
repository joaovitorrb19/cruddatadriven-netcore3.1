using System.ComponentModel.DataAnnotations;

namespace appteste{
    public class Tipo{
        [Key]
        public int Id {get;set;}
        [Required]
        public string nomeTipo {get;set;}
    
        [Required]
        public string descricao {get;set;}
    }
}