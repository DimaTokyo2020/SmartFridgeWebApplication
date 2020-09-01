using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFridge.Models; // пространство имен моделей и контекста данных
using System.Diagnostics;
using System;


namespace SmartFridge.Controllers
{
    public class HomeController : Controller
    {
        DevicesContext db;
        public HomeController(DevicesContext context)
        {
            this.db = context;

                        
            

            /*
            if (db.Devices.Count() == 0)
            {

                Device rosberyPi1 = new Device { Name = "Dimmmas_Rosbery" };
                Device rosberyPi2 = new Device { Name = "Mxs_Rosbery" };
               

                Product product1 = new Product { Name = "Tomato", Device = rosberyPi1, Count = 19 };
                Product product2 = new Product { Name = "Cucumber", Device = rosberyPi1, Count = 2 };
                Product product3 = new Product { Name = "Beer", Device = rosberyPi1, Count = 3 };
                Product product4 = new Product { Name = "Beet", Device = rosberyPi1, Count = 5 };
                Product product5 = new Product { Name = "Egg", Device = rosberyPi2, Count = 1 };
                Product product6 = new Product { Name = "Tomato", Device = rosberyPi2, Count = 0 };
                Product product7 = new Product { Name = "Cucumber", Device = rosberyPi2, Count = 6 };
                Product product8 = new Product { Name = "Beet", Device = rosberyPi2, Count = 21 };

                rosberyPi1.Products.Add(product1);
                rosberyPi1.Products.Add(product2);
                rosberyPi1.Products.Add(product3);
                rosberyPi1.Products.Add(product4);
                rosberyPi2.Products.Add(product5);
                rosberyPi2.Products.Add(product6);
                rosberyPi2.Products.Add(product7);
                rosberyPi2.Products.Add(product8);

                NotificationFromDevice notification1 = new NotificationFromDevice { Massage = "in" , Product = product1 };
                NotificationFromDevice notification2 = new NotificationFromDevice { Massage = "out", Product = product2 };
                NotificationFromDevice notification3 = new NotificationFromDevice { Massage = "out", Product = product3 };
                NotificationFromDevice notification4 = new NotificationFromDevice { Massage = "in", Product = product4 };
                NotificationFromDevice notification5 = new NotificationFromDevice { Massage = "in", Product = product5 };
                NotificationFromDevice notification6 = new NotificationFromDevice { Massage = "out", Product = product6 };
                NotificationFromDevice notification7 = new NotificationFromDevice { Massage = "out", Product = product7 };
                NotificationFromDevice notification8 = new NotificationFromDevice { Massage = "in", Product = product8 };

                product1.UpdatesList.Add(notification1);
                product2.UpdatesList.Add(notification2);
                product3.UpdatesList.Add(notification3);
                product4.UpdatesList.Add(notification4);
                product5.UpdatesList.Add(notification5);
                product6.UpdatesList.Add(notification6);
                product7.UpdatesList.Add(notification7);
                product8.UpdatesList.Add(notification8);


                db.Devices.AddRange(rosberyPi1, rosberyPi2);
                db.Products.AddRange(product1, product2, product3, product4, product5, product6, product7, product8);
                db.NotificationFromDevices.AddRange(notification1, notification2, notification3, notification4, notification5,
                    notification6, notification7, notification8);
                db.SaveChanges();
            }*/

            
            
        }
     

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Entry()
        {
            return View();
        }

        public IActionResult Delete(int Id)
        {
            //here, get the student from the database in the real application

            //getting a student from collection for demo purpose
            var std = db.Products.Where(s => s.Id == Id).FirstOrDefault();
            db.Products.Remove(std);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
        {
            IQueryable<Product> products = db.Products.Include(x => x.Device);

            ViewData["NameSort"] = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            ViewData["DeviceNameSort"] = sortOrder == SortState.DeviceNAmeAsc ? SortState.DeviceNAmeDesc : SortState.DeviceNAmeAsc;
            ViewData["CountSort"] = sortOrder == SortState.CountAsc ? SortState.CountDesc : SortState.CountAsc;

            products = sortOrder switch
            {
                SortState.NameDesc => products.OrderByDescending(s => s.Name),
                SortState.CountAsc => products.OrderBy(s => s.Count),
                SortState.CountDesc => products.OrderByDescending(s => s.Count),
                SortState.DeviceNAmeAsc => products.OrderBy(s => s.Device.Name),
                SortState.DeviceNAmeDesc => products.OrderByDescending(s => s.Device.Name),
                _ => products.OrderBy(s => s.Name),
            };
            return View(await products.AsNoTracking().ToListAsync());
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public ActionResult Edit(int Id)
        {
            //here, get the student from the database in the real application

            //getting a student from collection for demo purpose
            var std = db.Products.Where(s => s.Id == Id).FirstOrDefault();

            return View(std);
        }

        [HttpPost]
        public ActionResult Edit(Product std)
        {
            //update student in DB using EntityFramework in real-life application

            //update list by removing old student and adding updated student for demo purpose
            var student = db.Products.Where(s => s.Id == std.Id).FirstOrDefault();
            student.Count = std.Count;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}