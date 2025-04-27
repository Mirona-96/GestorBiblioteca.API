using GestorBiblioteca.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorBiblioteca.Infrastructure.Persistence.Configurations
{
    public class EmprestimoConfigurations : IEntityTypeConfiguration<Emprestimo>
    {
        public void Configure(EntityTypeBuilder<Emprestimo> builder)
        {
            builder
                .HasKey(e => e.Id);

            //1:N - um usuario pode realizar varios emprestimos
            builder
                .HasOne(e => e.Usuario)
                .WithMany(u => u.Emprestimos)
                .HasForeignKey(e => e.IdUsuario)
                .OnDelete(DeleteBehavior.Restrict); //impede eliminacao de uma entidade que tem relacionamento com outras

            //1:N - sobre um livro podem ser realizados varios emprestimos
            builder
                .HasOne(l => l.Livro)
                .WithMany(e => e.emprestimos)
                .HasForeignKey(e => e.IdLivro)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
