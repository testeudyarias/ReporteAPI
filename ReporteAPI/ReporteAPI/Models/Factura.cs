namespace ReporteAPI.Models
{
    public class Factura
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string NombreCliente { get; set; } = String.Empty;
        public string CodigoCliente { get; set; } = "";
        public string CodigoNombre { get { return CodigoCliente + " - " + NombreCliente; }  }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Monto { get; set; } = 0;
    }

    public class FacturaDTO
    {
        public string NombreCliente { get; set; } = String.Empty;
        public string CodigoCliente { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
        public string FechaDesde { get; set; } = string.Empty;
        public string FechaHasta { get; set; } = string.Empty;

    }
}
