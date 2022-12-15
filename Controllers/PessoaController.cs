
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appteste{

    [Route("pessoa")]
    public class PessoaController : ControllerBase{
        
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Pessoa>>> Get([FromServices]DataContext context){
        
            var aux = await context.Pessoas.Include("Tipo").AsNoTracking().ToListAsync();
            return aux;
            
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Pessoa>> Post([FromServices]DataContext context,[FromBody]Pessoa pessoa){

                if(!ModelState.IsValid){
                        return BadRequest(new{message="Dados Invalidos!"});
                }

                context.Pessoas.Add(pessoa);
                await context.SaveChangesAsync();
                return Ok(pessoa);
        }

    }
}