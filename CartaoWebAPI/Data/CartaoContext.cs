using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CartaoWebAPI.Models; 

namespace CartaoWebAPI.Data
{
    public class CartaoContext : DbContext
    {
        //Dbset pode ser considerado uma lista de objetos da Classe (Usuario, Cartao)
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cartao> Cartaos { get; set; }
        //CartaoContext para referenciar em services no arquivo startups.cs
        public CartaoContext(DbContextOptions<CartaoContext> options) : base(options) { }

        
    }
}
