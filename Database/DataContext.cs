using Microsoft.EntityFrameworkCore;

namespace appteste{
    public class DataContext : DbContext {
            public DataContext( DbContextOptions<DataContext> options) : base (options){

            }

            public DbSet<Pessoa> Pessoas {get;set;}
            public DbSet<Usuario> Usuarios {get;set;}
            public DbSet<Tipo> Tipos {get;set;}
    }
}