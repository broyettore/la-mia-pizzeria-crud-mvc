using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
