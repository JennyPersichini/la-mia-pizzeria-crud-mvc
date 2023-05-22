using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        // ACTIONS PER LA CREAZIONE DI UNA NUOVA PIZZA
        [Authorize(Roles = "ADMIN")]
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

        // ACTIONS PER LA MODIFICA DI UNA PIZZA
        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaModificata = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaModificata != null)
                {
                    return View("Update", pizzaModificata);
                }
                else
                {
                    return NotFound("Articolo inesistente!");
                }

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id, Pizza modificaPizza)
        {
            if (!ModelState.IsValid)
            {
                return View("Update", modificaPizza);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaDaModificare = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDaModificare != null)
                {
                    pizzaDaModificare.Immagine = modificaPizza.Immagine;
                    pizzaDaModificare.Nome = modificaPizza.Nome;
                    pizzaDaModificare.Prezzo = modificaPizza.Prezzo;
                    pizzaDaModificare.Descrizione = modificaPizza.Descrizione;                   

                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Pizza non esistente!");
                }

            }

        }

        // ACTIONS PER ELIMINARE UNA PIZZA
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Delete(int id)
        {
            using (PizzeriaContext db = new PizzeriaContext())
            {
                Pizza? pizzaDaEliminare= db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                if (pizzaDaEliminare != null)
                {
                    db.Remove(pizzaDaEliminare);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("Non c'è nulla da eliminare!");
                }

            }

        }

    }

}
