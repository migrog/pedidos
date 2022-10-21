using pedidos.dominio.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace pedidos.infra.data.mapping
{
    public class ProductoMap : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> entity)
        {
            entity.ToTable("PRODUCTO");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.MonedaEnum)
                .HasColumnName("MONEDA_ENUM")
                .HasMaxLength(20);

            entity.Property(e => e.Nombre)
                .HasColumnName("NOMBRE")
                .HasMaxLength(50);

            entity.Property(e => e.PrecioUnitario)
                .HasColumnName("PRECIO_UNITARIO")
                .HasColumnType("decimal(18, 2)");
        }
    }
}
