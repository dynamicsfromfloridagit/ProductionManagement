using Microsoft.AspNetCore.Mvc;
using ProductionManagement.Web.Data;
using ProductionManagement.Web.Models;

namespace ProductionManagement.Web.Controllers
{
    public class JobskuController(ApplicationDbContext _db, IHttpClientFactory _httpClientFactory) : Controller
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

        public IActionResult Delete(int id)
        {
            // this delete with  db contextof this project
            var objeto = _db.Jobskus.Find(id);
            if (objeto != null)
            {
                _db.Jobskus.Remove(objeto);
                _db.SaveChanges();
            }
           
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteWithFunction(int id)
        {
            // this delete with FunctionOne
            using var client = _httpClientFactory.CreateClient();
           
            //client.BaseAddress = new Uri("http://localhost:7073/api/");
            client.BaseAddress = new Uri("https://functappkokeproductionmanage.azurewebsites.net/api/");

            await client.GetAsync($"FunctionOne?id={id}");

            return RedirectToAction("Index");
        }
    }
}
