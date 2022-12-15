using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Collections.Generic;

namespace appteste{

    [Route("v1/usuarios")]
    public class UsuarioController : Controller {

        [HttpGet]
        [Route("")]
        [Authorize(Roles = "dev")]
        public async Task<ActionResult<List<Usuario>>> Get([FromServices]DataContext context){
            var aux = await context.Usuarios.AsNoTracking().ToListAsync();

            return aux;
        }

        [HttpPut]
        [Route("")]
        [Authorize(Roles ="dev")]
        public async Task<ActionResult<Usuario>> Put([FromBody]Usuario usuario, [FromServices]DataContext context){
                if(!ModelState.IsValid){
                    return BadRequest();
                }

                var aux = context.Usuarios.FirstOrDefault(x => x.Id == usuario.Id);

                if (aux == null){
                    return NotFound();
                
                }

                try {
                    context.Entry(usuario).State = EntityState.Modified;
                    await context.SaveChangesAsync();
                    return usuario;

                } catch(Exception e){
                        return BadRequest(e.Message);
                }
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<ActionResult<Usuario>> Post([FromServices]DataContext context,[FromBody]Usuario usuario){
            if(!ModelState.IsValid){
                return BadRequest();
            }

            try {
            context.Usuarios.Add(usuario);
            await context.SaveChangesAsync();
            return usuario;
            } catch(Exception e){
                    return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate([FromServices]DataContext context,[FromBody]Usuario usuario){
            var auxUsuario = await context.Usuarios.AsNoTracking().Where(x => x.usuario == usuario.usuario && x.password == usuario.password).FirstOrDefaultAsync();

            if(auxUsuario == null){
                return NotFound();
            }

            var token = TokenServices.GenerateToken(auxUsuario);
            return new {usuario = usuario, token = token};
        }

      

    }
}