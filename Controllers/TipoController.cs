
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace appteste{

    [Route("v1/tipo")]
    public class TipoController : ControllerBase{
        
        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<List<Tipo>>> Get([FromServices]DataContext context){
            var aux = await context.Tipos.AsNoTracking().ToListAsync();
            return aux;
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "dev")]
        public async Task<ActionResult<Tipo>> Post([FromServices]DataContext context,[FromBody]Tipo tipo){

                if(!ModelState.IsValid){
                        return BadRequest(new{message="Dados Invalidos!"});
                }

              
              try{
                context.Tipos.Add(tipo);
                await context.SaveChangesAsync();
                return Ok(tipo);
              } catch (Exception e){
                return BadRequest(e.Message);
              }
        }

        [HttpPut]
        [Route("")]
        [Authorize(Roles = "dev")]
        public async Task<ActionResult<Tipo>> Put([FromBody]Tipo tipo,[FromServices]DataContext context){
            
            try{
             context.Entry(tipo).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return tipo;
            }catch(Exception e){
                    return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("")]
        [Authorize(Roles = "dev")]
        public async Task<ActionResult<Tipo>> Delete([FromBody]Tipo tipo,[FromServices]DataContext context){
                var aux = context.Tipos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == tipo.Id);

                if(!ModelState.IsValid) return BadRequest();
                
                if (aux == null){
                    return NotFound();
                }

                var auxTipo = context.Tipos.Remove(tipo);
                await context.SaveChangesAsync();
                return Ok(auxTipo);
        }
    }
}