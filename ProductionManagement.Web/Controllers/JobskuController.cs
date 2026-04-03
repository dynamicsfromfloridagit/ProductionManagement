using Microsoft.AspNetCore.Mvc;
using ProductionManagement.Web.Data;
using ProductionManagement.Web.Models;

namespace ProductionManagement.Web.Controllers
{
    public class JobskuController(ApplicationDbContext _db) : Controller
    {
        public IActionResult Index()
        {
            List<Jobsku> objList = _db.Jobskus.ToList();
            return View(objList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Jobsku _obj)
        {
            if (ModelState.IsValid) {
                _db.Jobskus.Add(_obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
           return View();
        }
    }
}
