using System.Collections.Generic;
using System.Linq;

namespace temperature_conversion_app_ef_core.Models
{
    public class Metric
    {
        public int MetricId { get; set; }
        public string Title { get; set; }

        public static List<string> GetMetrics()
        {
            TemperatureConversionDbContext temperatureConversionAppDbContext = new TemperatureConversionDbContext();
            List<string> metrics = temperatureConversionAppDbContext.Metrics.Select(m => m.Title).ToList();
            return metrics;
        }
    }
}
