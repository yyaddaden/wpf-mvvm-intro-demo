using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace temperature_conversion_app_ef_core.Models
{
    public class Metric
    {
        public int MetricId { get; set; }
        public string Title { get; set; }

        public static List<string> GetMetrics()
        {
            TemperatureConversionAppDbContext temperatureConversionAppDbContext = new TemperatureConversionAppDbContext();
            List<string> metrics = temperatureConversionAppDbContext.Metrics.Select(m => m.Title).ToList();
            return metrics;
        }
    }
}
