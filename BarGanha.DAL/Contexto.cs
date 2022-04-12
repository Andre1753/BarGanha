using BarGanha.BLL.Models;
using BarGanha.DAL.Mapeamentos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BarGanha.DAL
{
    public class Contexto : IdentityDbContext<Usuario, Funcao, string>
    {
        public DbSet<Funcao> Funcoes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Oferta> Ofertas { get; set; }
        public DbSet<Troca> Trocas { get; set; }
        public DbSet<ProdutoOfertado> ProdutosOfertados { get; set; }


        public Contexto(DbContextOptions<Contexto> opcoes) : base(opcoes)
        {
            Database.EnsureCreated();
        }   

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new FuncaoMap());
            builder.ApplyConfiguration(new UsuarioMap());
            builder.ApplyConfiguration(new ProdutoMap());
            builder.ApplyConfiguration(new CategoriaMap());
            builder.ApplyConfiguration(new OfertaMap());
            builder.ApplyConfiguration(new TrocaMap());
            builder.ApplyConfiguration(new ProdutoOfertadoMap());

        }
    }
}
