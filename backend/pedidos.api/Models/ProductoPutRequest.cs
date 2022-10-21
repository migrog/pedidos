namespace pedidos.api.Models
{
    public class ProductoPutRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }
        public string MonedaEnum { get; set; }
    }
}
