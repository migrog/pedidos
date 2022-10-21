namespace pedidos.dominio.entidades.dto
{
    public class ProductoSearchResponse
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double PrecioUnitario { get; set; }

        public string Moneda { get; set; }
    }
}
