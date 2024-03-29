using System.ComponentModel.DataAnnotations;

namespace WebAPI
{
    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }

    }

    public class Order
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public Decimal Sum { get; set; }
        [Required]
        public string Customer { get; set; }
    }
}