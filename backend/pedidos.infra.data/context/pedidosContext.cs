using System;
using System.Collections.Generic;
using System.Text;
using pedidos.dominio.entidades;
using pedidos.infra.data.mapping;
using Microsoft.EntityFrameworkCore;

namespace pedidos.infra.data.context
{
    public partial class pedidosContext : DbContext
    {
        public pedidosContext() { }
        public pedidosContext(DbContextOptions<pedidosContext> options) : base(options) { }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoDetalle> PedidoDetalle { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                var cnx = new AppConfiguration().SqlDataConnection;
                optionsBuilder.UseSqlServer(cnx);
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(entity => new PedidoMap().Configure(entity));
            modelBuilder.Entity<PedidoDetalle>(entity => new PedidoDetalleMap().Configure(entity));
            modelBuilder.Entity<Producto>(entity => new ProductoMap().Configure(entity));
            modelBuilder.Entity<Cliente>(entity => new ClienteMap().Configure(entity));

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
