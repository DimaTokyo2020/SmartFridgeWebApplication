using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartFridge.Models;

namespace SmartFridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationFromDevicesController : ControllerBase
    {
        private readonly DevicesContext _context;

        public NotificationFromDevicesController(DevicesContext context)
        {
            _context = context;
        }

        // GET: api/NotificationFromDevices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationFromDevice>>> GetNotificationFromDevices()
        {
            return await _context.NotificationFromDevices.ToListAsync();
        }

        // GET: api/NotificationFromDevices/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotificationFromDevice>> GetNotificationFromDevice(int id)
        {
            var notificationFromDevice = await _context.NotificationFromDevices.FindAsync(id);

            if (notificationFromDevice == null)
            {
                return NotFound();
            }

            return notificationFromDevice;
        }

        // PUT: api/NotificationFromDevices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotificationFromDevice(int id, NotificationFromDevice notificationFromDevice)
        {
            if (id != notificationFromDevice.Id)
            {
                return BadRequest();
            }

            _context.Entry(notificationFromDevice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotificationFromDeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/NotificationFromDevices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<NotificationFromDevice>> PostNotificationFromDevice(NotificationFromDevice notificationFromDevice)
        {

            char[] spearator = { ',' };
            string[] massages = notificationFromDevice.Massage.Split(spearator);
            Boolean isANewDevice = false;
            int id = notificationFromDevice.ProductId;
            Product product = _context.Products.Find(id);
            if (product == null)
            {
                Device device = _context.Devices.Find(Int32.Parse(massages[1]));
                if (device == null)
                {
                    device = new Device { Id = Int32.Parse(massages[1]), Name = massages[0], UserRosbery = massages[0] };
                    isANewDevice = true;
                }
                product = new Product { Id = notificationFromDevice.ProductId, Name = massages[2], Device = device, Count = 1 };


                if (isANewDevice)
                {

                    _context.Devices.Add(device);
                    try
                    {
                        _context.Database.OpenConnection();
                        _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Devices ON");
                        _context.SaveChanges();
                        _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Devices OFF");


                        _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Products ON");
                        _context.Products.Add(product);
                        _context.SaveChanges();
                        _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Products OFF");


                    }
                    finally
                    { _context.Database.CloseConnection(); }
                }
                else { 
                    
                    
                    try
                    {
                        _context.Database.OpenConnection();
                        _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Products ON");
                        _context.Products.Add(product);
                        _context.SaveChanges();
                        _context.Database.ExecuteSqlCommand("SET IDENTITY_INSERT dbo.Products OFF");
                        _context.Devices.Add(device);

                    }
                    finally
                    { _context.Database.CloseConnection(); }
                }
                device.Products.Add(product);
            }
            else
            {
                if (massages[3].Equals("in")) { product.Count += 1; }
                else { 
                    if(product.Count > 0) { product.Count -= 1; }
                    else { product.Count = 0; }
                    }
            }
            
            _context.NotificationFromDevices.Add(notificationFromDevice);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotificationFromDevice", new { id = notificationFromDevice.Id }, notificationFromDevice);
        }


        // DELETE: api/NotificationFromDevices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NotificationFromDevice>> DeleteNotificationFromDevice(int id)
        {
            var notificationFromDevice = await _context.NotificationFromDevices.FindAsync(id);
            if (notificationFromDevice == null)
            {
                return NotFound();
            }

            _context.NotificationFromDevices.Remove(notificationFromDevice);
            await _context.SaveChangesAsync();

            return notificationFromDevice;
        }

        private bool NotificationFromDeviceExists(int id)
        {
            return _context.NotificationFromDevices.Any(e => e.Id == id);
        }
    }
}
