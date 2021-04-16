using System.Collections.Generic;
using System.Linq;

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
            TemperatureConversionDbContext temperatureConversionAppDbContext = new TemperatureConversionDbContext();
            List<User> users = temperatureConversionAppDbContext.Users.ToList();
            return users;
        }

        public static void AddUser(User user)
        {
            TemperatureConversionDbContext temperatureConversionAppDbContext = new TemperatureConversionDbContext();
            temperatureConversionAppDbContext.Users.Add(user);
            temperatureConversionAppDbContext.SaveChanges();
        }
    }
}
