namespace pedidos.api.Models
{
    public class ProductoPostRequest
    {
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }
        public string MonedaEnum { get; set; }
    }
}
