using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace temperature_conversion_app_ef_core.Models
{
    public class User 
    {
        public User()
        {
            Conversions = new List<Conversion>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }

        public ICollection<Conversion> Conversions { get; set; }

        public static List<User> GetUsers()
        {
            TemperatureConversionAppDbContext temperatureConversionAppDbContext = new TemperatureConversionAppDbContext();
            List<User> users = temperatureConversionAppDbContext.Users.ToList();
            return users;
        }

        public static void AddUser(User user)
        {
            TemperatureConversionAppDbContext temperatureConversionAppDbContext = new TemperatureConversionAppDbContext();
            temperatureConversionAppDbContext.Users.Add(user);
            temperatureConversionAppDbContext.SaveChanges();
        }
    }
}
