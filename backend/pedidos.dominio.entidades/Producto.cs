namespace pedidos.dominio.entidades
{
    public class Producto: EntidadBase
    {
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }
        public string MonedaEnum { get; set; }
    }
}
