using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Mvc;

namespace LaMiaPizzeria.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                List<Pizza> ListaPizze = db.Pizze.ToList();

                return View("Index", ListaPizze);
            }

        }

        public IActionResult Dettagli(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaDettagli = db.Pizze.Where(article => article.Id == id).FirstOrDefault();

                if (pizzaDettagli != null)
                {
                    return View("Dettagli", pizzaDettagli);
                }
                else
                {
                    return NotFound("Pizza non trovata!");
                }
            }


        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", newPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Pizze.Add(newPizza);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

        }
    }
}
