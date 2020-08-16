using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFridge.Models; // пространство имен моделей и контекста данных

namespace SmartFridge.Controllers
{
    public class HomeController : Controller
    {
        private UsersContext db;
        public HomeController(UsersContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Users.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}