using Microsoft.AspNetCore.Mvc;

namespace ReporteAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private List<Models.Factura> facturas = new List<Models.Factura>() { 
            new Models.Factura{ 
               NombreCliente="pepe",
               CodigoCliente="1",
               Fecha= new  DateTime(2022,07,20),
               Usuario = "julio.arias"
            },
            new Models.Factura{
               NombreCliente="pedro",
               CodigoCliente="1",
               Fecha= new  DateTime(2022,07,20),
               Usuario = "julio.arias"
            },
            new Models.Factura{
               NombreCliente="matrio",
               CodigoCliente="1",
               Fecha= new  DateTime(2022,07,20),
               Usuario = "julio.arias"
            },
            new Models.Factura{
               NombreCliente="neyfry ",
               CodigoCliente="2",
               Fecha= new  DateTime(2022,08,20),
               Usuario = "julio.arias"
            },
        }; 

        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("Facturas")]
        public ActionResult GetFacturas() {

            try
            {
                return Ok(facturas);
            }
            catch (Exception e)
            {
                return BadRequest("Error "+e.Message);
            }
        }
        [HttpGet("Facturas/{codigoCliente}")]
        public ActionResult GetFacturas(string codigoCliente)
        {

            try
            {
                var aux = facturas;
                if (codigoCliente != string.Empty)
                {
                    aux = aux.Where(x => x.CodigoCliente == codigoCliente).ToList();
                }
                return Ok(aux);
            }
            catch (Exception e)
            {
                return BadRequest("Error " + e.Message);
            }
        }
        [HttpPost("Facturas/Buscar")]
        public ActionResult BuscarFactura([FromBody]Models.FacturaDTO dTO)
        {

            try
            {
                var aux = facturas;
                if (dTO.FechaDesde != string.Empty && dTO.FechaHasta != string.Empty)
                {
                    //08/10/2022
                    //2022/10/08
                    //08-10-2022
                    //2022-10-08
                    //20221008
                    //SAP
                    if (dTO.FechaDesde.Length == 8 && dTO.FechaHasta.Length == 8)
                    {
                        string desde = dTO.FechaDesde;
                        string desdeY = desde.Substring(0,4);
                        string desdeM = desde.Substring(4,2);
                        string desdeD = desde.Substring(6,2);

                        var fDesde = new DateTime(
                                int.Parse(desdeY),
                                int.Parse(desdeM),
                                int.Parse(desdeD)
                            );


                        string hasta = dTO.FechaHasta;
                        string hastaY = hasta.Substring(0, 4);
                        string hastaM = hasta.Substring(4, 2);
                        string hastaD = hasta.Substring(6, 2);

                        var fHasta = new DateTime(
                                int.Parse(hastaY),
                                int.Parse(hastaM),
                                int.Parse(hastaD)
                            );
                        aux = aux.Where(x => x.Fecha >= fDesde && x.Fecha <= fHasta).ToList();
                    }

                    
                }
                if (dTO.CodigoCliente != string.Empty)
                {
                    aux = aux.Where(x => x.CodigoCliente == dTO.CodigoCliente).ToList();
                }
                if (dTO.NombreCliente != string.Empty)
                {
                    aux = aux.Where(x => x.NombreCliente.Contains(dTO.NombreCliente)).ToList();
                }

                return Ok(aux);
            }
            catch (Exception e)
            {
                return BadRequest("Error " + e.Message);
            }
        }

    }
}