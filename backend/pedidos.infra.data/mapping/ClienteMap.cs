using pedidos.dominio.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace pedidos.infra.data.mapping
{
    public class ClienteMap : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> entity)
        {
            entity.ToTable("CLIENTE");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Nombre)
                .HasColumnName("NOMBRE")
                .HasMaxLength(50);
        }
    }
}
