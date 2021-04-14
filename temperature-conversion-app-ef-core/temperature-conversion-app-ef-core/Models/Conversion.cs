using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace temperature_conversion_app_ef_core.Models
{
    public class Conversion
    {
        public int ConversionId { get; set; }
        public string InitialMetric { get; set; }
        public float? InitialValue { get; set; }
        public string FinalMetric { get; set; }
        public float? FinalValue { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }

        public static List<Conversion> GetConversions(User user)
        {
            TemperatureConversionAppDbContext temperatureConversionAppDbContext = new TemperatureConversionAppDbContext();
            List<Conversion> conversions = temperatureConversionAppDbContext.Conversions.Where(c => c.User.Name == user.Name).ToList();
            return conversions;
        }

        public static void AddConversion(Conversion conversion, User user)
        {
            TemperatureConversionAppDbContext temperatureConversionAppDbContext = new TemperatureConversionAppDbContext();
            user = temperatureConversionAppDbContext.Users.Where(u => u.Name == user.Name).First();
            conversion.UserId = user.UserId;
            temperatureConversionAppDbContext.Conversions.Add(conversion);
            temperatureConversionAppDbContext.SaveChanges();
        }

        public static void RemoveConversion(int conversionId)
        {
            TemperatureConversionAppDbContext temperatureConversionAppDbContext = new TemperatureConversionAppDbContext();
            Conversion conversion = temperatureConversionAppDbContext.Conversions.Find(conversionId);
            temperatureConversionAppDbContext.Conversions.Remove(conversion);
            temperatureConversionAppDbContext.SaveChanges();
        }
    }
}