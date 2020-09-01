using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFridge.Models
{
    public class NotificationFromDevice
    {
        public int Id { get; set; }
        public string Massage { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
