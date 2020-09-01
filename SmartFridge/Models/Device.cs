using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Product> Products { get; set; }
        public string UserRosbery { get; set; }
        public Device()
        {
            Products = new List<Product>();
        }
    }
}
