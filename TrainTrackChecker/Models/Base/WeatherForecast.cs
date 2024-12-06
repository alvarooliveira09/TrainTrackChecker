namespace TrainTrackChecker.Models
{

    public class WeatherForecast
    {
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }

        public List<Forecast>? Forecasts { get; set; }
    }

    public class Forecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
