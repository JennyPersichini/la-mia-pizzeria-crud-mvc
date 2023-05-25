using LaMiaPizzeria.Database;
using LaMiaPizzeria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;
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
            using PizzeriaContext db = new();
            List<PizzaCategory> Categorie = db.Categorie.ToList();

            CategoriaPizzaView modelloView = new()
            {
                Pizza = new Pizza(),
                Categorie = Categorie,
            };

            return View("Create", modelloView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoriaPizzaView data)
        {
            if (!ModelState.IsValid)
            {
                using PizzeriaContext db = new();
                List<PizzaCategory> categorie = db.Categorie.ToList();
                data.Categorie = categorie;

                return View("Create", data);
            }

            using (PizzeriaContext db = new PizzeriaContext())
            {
                db.Pizze.Add(data.Pizza);
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
                    List<PizzaCategory> Categorie = db.Categorie.ToList();
                    CategoriaPizzaView modelloView = new()
                    {
                        Pizza = new Pizza(),
                        Categorie = Categorie,
                    };

                    return View("Update", modelloView);
                }
                else
                {
                    return NotFound("Pizza inesistente!");
                }

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public IActionResult Update(int id, PizzaCategory data)
        {
            if (!ModelState.IsValid)
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    List<PizzaCategory> PizzaCategory = db.PizzaCategorie.ToList();
                    data.PizzaCategorie = PizzaCategory;

                    return View("Update", data);
                }
            }
            else
            {
                using (PizzeriaContext db = new PizzeriaContext())
                {
                    Pizza? pizzaDaModificare = db.Pizze.Where(pizza => pizza.Id == id).FirstOrDefault();

                    if (pizzaDaModificare != null)
                    {
                        pizzaDaModificare.Immagine = PizzaCategory.Immagine;
                        pizzaDaModificare.Nome = PizzaCategory.Nome;
                        pizzaDaModificare.Prezzo = PizzaCategory.Prezzo;
                        pizzaDaModificare.Descrizione = PizzaCategory.Descrizione;

                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return NotFound("Pizza non esistente!");
                    }
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
