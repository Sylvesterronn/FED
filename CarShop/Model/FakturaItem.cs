using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace CarShop.Models
{
    public class FakturaItem
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string mechanicName { get; set; }
        public string materials { get; set; }
        public double materialsCost { get; set; }
        public double hours { get; set; }
        public double hourlyRate { get; set; }
    }
}