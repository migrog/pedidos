using pedidos.dominio.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace pedidos.infra.data.mapping
{
    public class PedidoMap : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> entity)
        {
            entity.ToTable("PEDIDO");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.FechaEmision)
                .HasColumnName("FECHA_EMISION")
                .HasColumnType("date");

            entity.Property(e => e.FechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("datetime");

            entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

            entity.Property(e => e.MonedaEnum)
                .HasColumnName("MONEDA_ENUM")
                .HasMaxLength(20);

            entity.Property(e => e.Total)
                .HasColumnName("TOTAL")
                .HasColumnType("decimal(18, 2)");
        }
    }
}
