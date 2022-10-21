using pedidos.dominio.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace pedidos.infra.data.mapping
{
    public class PedidoDetalleMap : IEntityTypeConfiguration<PedidoDetalle>
    {
        public void Configure(EntityTypeBuilder<PedidoDetalle> entity)
        {
            entity.ToTable("PEDIDO_DETALLE");

            entity.Property(e => e.Id)
                .HasColumnName("ID")
                .HasMaxLength(10)
                .IsFixedLength();

            entity.Property(e => e.Cantidad)
                .HasColumnName("CANTIDAD")
                .HasColumnType("decimal(18, 2)");

            entity.Property(e => e.IdPedido).HasColumnName("ID_PEDIDO");

            entity.Property(e => e.IdProducto).HasColumnName("ID_PRODUCTO");

            entity.Property(e => e.PrecioUnitario)
                .HasColumnName("PRECIO_UNITARIO")
                .HasColumnType("decimal(18, 2)");
        }
    }
}
