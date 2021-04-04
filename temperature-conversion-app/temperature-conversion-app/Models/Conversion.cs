using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace temperature_conversion_app.Models
{
    public class Conversion
    {
        public string InitialMetric { get; set; }
        public float? InitialValue { get; set; }
        public string FinalMetric { get; set; }
        public float? FinalValue { get; set; }
    }
}