using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        public IActionResult Index()
        {
            using (PizzaContext db = new())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();

                return View("Index", pizzas);
            }
        }

        public IActionResult UserIndex()
        {
            using (PizzaContext db = new())
            {
                List<Pizza> pizzas = db.Pizzas.ToList<Pizza>();

                return View("UserIndex", pizzas);
            }
        }

        public IActionResult Details(int id)
        {
            using (PizzaContext db = new())
            {
                Pizza? foundPizza = db.Pizzas.Where(p => p.PizzaId == id).FirstOrDefault();

                if (foundPizza == null)
                {
                    return NotFound($"La pizza con {id} non è stata trovata!");
                }
                else
                {
                    return View("Details", foundPizza);
                }
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pizza newPizza)
        {
            if(!ModelState.IsValid)
            {
                return View("Create", newPizza);
            }

            using(PizzaContext db = new())
            {
                db.Pizzas.Add(newPizza);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            using(PizzaContext db = new())
            {
                Pizza? pizzaToEdit = db.Pizzas.Where(p => p.PizzaId == id).FirstOrDefault();


                if (pizzaToEdit == null)
                {
                    return NotFound("Pizza to edit was not found");
                }
                else
                {
                    return View("Update", pizzaToEdit);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Pizza newPizza)
        {
            if(!ModelState.IsValid)
            {
                return View("Update", newPizza);
            }

            using(PizzaContext db = new())
            {
                Pizza? pizzaToEdit = db.Pizzas.Find(id);

                if(pizzaToEdit != null)
                {
                    EntityEntry<Pizza> entryEntity = db.Entry(pizzaToEdit);
                    entryEntity.CurrentValues.SetValues(newPizza);

                    db.SaveChanges();
                    return RedirectToAction("Index");
                } else
                {
                    return NotFound("The pizza you want to edit was not found");
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            using(PizzaContext db = new())
            {
                Pizza? pizzaToDelete = db.Pizzas.Where(p => p.PizzaId == id).FirstOrDefault();

                if (pizzaToDelete != null)
                {
                    db.Pizzas.Remove(pizzaToDelete);
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    return NotFound("The pizza was not found");
                }
            }
        }
    }
}
