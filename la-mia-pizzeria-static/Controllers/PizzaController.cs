using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace la_mia_pizzeria_static.Controllers
{
    public class PizzaController : Controller
    {
        private readonly PizzaContext _myDb;
        private readonly ILogger<PizzaController> _logger;
        public PizzaController(PizzaContext db, ILogger<PizzaController> logger)
        {
            _myDb = db;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Pizza> pizzas = _myDb.Pizzas.ToList<Pizza>();

            return View("Index", pizzas);
        }

        public IActionResult UserIndex()
        {
            List<Pizza> pizzas = _myDb.Pizzas.ToList<Pizza>();

            return View("UserIndex", pizzas);
        }

        public IActionResult Details(int id)
        {
            Pizza? foundPizza = _myDb.Pizzas.Where(p => p.PizzaId == id).Include(p => p.Category).FirstOrDefault();

            if (foundPizza == null)
            {
                return NotFound($"La pizza con {id} non è stata trovata!");
            }
            else
            {
                return View("Details", foundPizza);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _myDb.Categories.ToList();

            PizzaFormModel model = new() { Pizza = new(), Categories = categories };

            return View("Create", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDb.Categories.ToList();
                data.Categories = categories;

                return View("Create", data);
            }

            _myDb.Pizzas.Add(data.Pizza);
            _myDb.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {

            Pizza? pizzaToEdit = _myDb.Pizzas.Where(p => p.PizzaId == id).FirstOrDefault();


            if (pizzaToEdit == null)
            {
                return NotFound("Pizza to edit was not found");
            }
            else
            {
                List<Category> categories = _myDb.Categories.ToList();

                PizzaFormModel model = new() { Pizza = pizzaToEdit, Categories = categories };

                return View("Update", model);
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            _logger.LogInformation("Attempting to update pizza with ID: {id}", id);

            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDb.Categories.ToList();
                data.Categories= categories;
                return View("Update", data);
            }


            Pizza? pizzaToEdit = _myDb.Pizzas.Find(id);

            if (pizzaToEdit != null)
            {
                _logger.LogInformation("Pizza found for update. ID: {id}", id);
                EntityEntry<Pizza> entryEntity = _myDb.Entry(pizzaToEdit);
                entryEntity.CurrentValues.SetValues(data.Pizza);

                _myDb.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogWarning("Pizza not found for update. ID: {id}", id);
                return NotFound("The pizza you want to edit was not found");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            Pizza? pizzaToDelete = _myDb.Pizzas.Where(p => p.PizzaId == id).FirstOrDefault();

            if (pizzaToDelete != null)
            {
                _myDb.Pizzas.Remove(pizzaToDelete);
                _myDb.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("The pizza was not found");
            }
        }
    }
}
