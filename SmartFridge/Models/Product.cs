using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<NotificationFromDevice> UpdatesList { get; set; }
        public int DeviceId { get; set; }
        public Device Device { get; set; }
        public int Count { get; set; }

        public Product()
        {
            UpdatesList = new List<NotificationFromDevice>();
        }
    }
}
