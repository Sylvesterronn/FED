using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CarShop.Models
{
    public class CarShopItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string customerName { get; set; }
        public string customerAddres { get; set; }  

        public string carBrand { get; set; }
        public string carModel { get; set; }
        public double RegistrationNumber { get; set; }
        public DateTime handInTime { get; set; }
        public string carProblem { get; set; }
        
    }
}