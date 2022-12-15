
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appteste{
 
    [Route("v1/pessoa")]
    public class PessoaController : ControllerBase{
        
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        [ResponseCache(VaryByHeader = "User-Agent", Location = ResponseCacheLocation.Any, Duration = 30)] // Essa linha deixa apenas esse metodo cacheado por 30minutos..
        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] > se deixar o services.addresponsecaching(); ligado, essa linha faz com que o metodo
        // nao seja cacheado...
        public async Task<ActionResult<List<Pessoa>>> Get([FromServices]DataContext context){
        
            var aux = await context.Pessoas.Include("Tipo").AsNoTracking().ToListAsync();
            return aux;
            
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "dev")]
        public async Task<ActionResult<Pessoa>> Post([FromServices]DataContext context,[FromBody]Pessoa pessoa){

                if(!ModelState.IsValid){
                        return BadRequest(new{message="Dados Invalidos!"});
                }

            try {
                context.Pessoas.Add(pessoa);
                await context.SaveChangesAsync();
                return Ok(pessoa);
            } catch (Exception e){
                    return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("")]
        [Authorize(Roles ="dev")]
        public async Task<ActionResult<Pessoa>> Put([FromBody]Pessoa pessoa,[FromServices]DataContext context){
                var auxPessoa = await context.Pessoas.AsNoTracking().FirstOrDefaultAsync(x => x.Id == pessoa.Id);
                if(!ModelState.IsValid || auxPessoa == null) return BadRequest();

                try {
                    context.Entry(pessoa).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return pessoa;

                }catch (Exception e){
                    return BadRequest(e.Message);
                }
        }
        
        [HttpDelete]
        [Route("")]
        [Authorize(Roles = "dev")]
        public async Task<ActionResult<Pessoa>> Delete([FromBody]Pessoa pessoa,[FromServices]DataContext context){
            var auxPessoa = await context.Pessoas.FirstOrDefaultAsync(x => x.Id == pessoa.Id);

            if(auxPessoa == null || !ModelState.IsValid) return BadRequest();

        try {
             context.Pessoas.Remove(auxPessoa);
             await context.SaveChangesAsync();
             return pessoa;
           } catch (Exception e) {
             return BadRequest(e.Message);
            }        
        }


    }
}