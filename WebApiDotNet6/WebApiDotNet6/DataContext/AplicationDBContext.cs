using Microsoft.EntityFrameworkCore;
using WebApiDotNet6.Models;

namespace WebApiDotNet6.DataContext
{
    public class AplicationDBContext : DbContext // DB CONTEXT VEM DO ENTITY FRAMEWORK
    {
        public AplicationDBContext(DbContextOptions<AplicationDBContext> options) : base(options)
        {
            
        }

        public DbSet<FuncionarioModel> Funcionarios { get; set; } // REFERENCIA QUAL É A MODEL PARA TRANSFORMAR EM TABELA
    }
}
