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
    public class LivroConfigurations : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder
                .HasKey(l => l.Id);

            builder
                .Property(l => l.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
