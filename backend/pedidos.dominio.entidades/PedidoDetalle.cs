namespace pedidos.dominio.entidades
{
    public class PedidoDetalle: EntidadBase
    {
        public int IdPedido { get; set; }
        public int IdProducto { get; set; }
        public double Cantidad { get; set; }
        public double PrecioUnitario { get; set; }
    }
}
