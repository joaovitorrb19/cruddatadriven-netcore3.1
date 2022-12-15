
using System.ComponentModel.DataAnnotations;

namespace appteste{
    public class Pessoa{
        [Key]
        public int Id {get;set;}
        [Required]        
        public string nome {get;set;}
        [Required]
        public string cpf {get;set;}
        [Required]
        public string dataCadastro {get;set;}
        
         [Required]
        public int TipoId {get;set;}
        public Tipo Tipo {get;set;}
       
    }
}