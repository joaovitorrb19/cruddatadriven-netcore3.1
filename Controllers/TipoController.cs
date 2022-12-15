
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace appteste{

    [Route("tipo")]
    public class TipoController : ControllerBase{
        
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Tipo>> Post([FromServices]DataContext context,[FromBody]Tipo tipo){

                if(!ModelState.IsValid){
                        return BadRequest(new{message="Dados Invalidos!"});
                }

                context.Tipos.Add(tipo);
                await context.SaveChangesAsync();
                return Ok(tipo);
        }

    }
}