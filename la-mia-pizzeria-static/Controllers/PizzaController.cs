using la_mia_pizzeria_static.Database;
using la_mia_pizzeria_static.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        [Authorize(Roles = "ADMIN")]
        public IActionResult Index()
        {
            List<Pizza> pizzas = _myDb.Pizzas
                                      .Include(p => p.Category)
                                      .Include(p => p.Ingredients)
                                      .ToList();
            return View("Index", pizzas);
        }

        public IActionResult UserIndex()
        {
            List<Pizza> pizzas = _myDb.Pizzas
                                      .Include(p => p.Category)
                                      .Include(p => p.Ingredients)
                                      .ToList();
            return View("UserIndex", pizzas);
        }

        public IActionResult Details(int id)
        {
            Pizza? foundPizza = _myDb.Pizzas.Where(p => p.PizzaId == id).Include(p => p.Category).Include(p => p.Ingredients).FirstOrDefault();

            if (foundPizza == null)
            {
                return NotFound($"La pizza con {id} non è stata trovata!");
            }
            else
            {
                return View("Details", foundPizza);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Create()
        {
            List<Category> categories = _myDb.Categories.ToList();
            List<SelectListItem> AllIngredientSelectList = new();
            List<Ingredient> dbAllIngredients = _myDb.Ingredients.ToList();

            foreach (Ingredient ingredient in dbAllIngredients)
            {
                AllIngredientSelectList.Add(
                    new SelectListItem
                    {
                        Text = ingredient.IngredientName,
                        Value = ingredient.IngredientID.ToString(),
                    });
            }

            PizzaFormModel model = new()
            {
                Pizza = new(),
                Categories = categories,
                Ingredients = AllIngredientSelectList
            };

            return View("Create", model);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PizzaFormModel data)
        {
            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDb.Categories.ToList();
                data.Categories = categories;

                List<SelectListItem> AllIngredientSelectList = new();
                List<Ingredient> dbAllIngredients = _myDb.Ingredients.ToList();

                foreach (Ingredient ingredient in dbAllIngredients)
                {
                    AllIngredientSelectList.Add(
                        new SelectListItem
                        {
                            Text = ingredient.IngredientName,
                            Value = ingredient.IngredientID.ToString(),
                        });
                }

                data.Ingredients = AllIngredientSelectList;

                return View("Create", data);
            }

            data.Pizza.Ingredients = new List<Ingredient>();

            if (data.SelectedIngredientsId != null)
            {
                foreach (string selectedIngredientId in data.SelectedIngredientsId)
                {
                    int intSelectedIngredientId = int.Parse(selectedIngredientId);

                    Ingredient? ingredientInDb = _myDb.Ingredients.Where(i => i.IngredientID == intSelectedIngredientId).FirstOrDefault();

                    if (ingredientInDb != null)
                    {
                        data.Pizza.Ingredients.Add(ingredientInDb);
                    }
                }
            }

            _myDb.Pizzas.Add(data.Pizza);
            _myDb.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet]
        public IActionResult Update(int id)
        {

            Pizza? pizzaToEdit = _myDb.Pizzas.Where(p => p.PizzaId == id).Include(p => p.Ingredients).FirstOrDefault();


            if (pizzaToEdit == null)
            {
                return NotFound("Pizza to edit was not found");
            }
            else
            {
                List<Category> categories = _myDb.Categories.ToList();
                List<SelectListItem> AllIngredientSelectList = new();
                List<Ingredient> dbAllIngredients = _myDb.Ingredients.ToList();

                foreach (Ingredient ingredient in dbAllIngredients)
                {
                    AllIngredientSelectList.Add(
                        new SelectListItem
                        {
                            Text = ingredient.IngredientName,
                            Value = ingredient.IngredientID.ToString(),
                            Selected = pizzaToEdit.Ingredients.Any(relatedIngredient => relatedIngredient.IngredientID == ingredient.IngredientID)
                        });
                }

                PizzaFormModel model = new()
                {
                    Pizza = pizzaToEdit,
                    Categories = categories,
                    Ingredients = AllIngredientSelectList
                };

                return View("Update", model);
            }

        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, PizzaFormModel data)
        {
            _logger.LogInformation("Attempting to update pizza with ID: {id}", id);

            if (!ModelState.IsValid)
            {
                List<Category> categories = _myDb.Categories.ToList();
                data.Categories = categories;

                List<SelectListItem> AllIngredientSelectList = new();
                List<Ingredient> dbAllIngredients = _myDb.Ingredients.ToList();

                foreach (Ingredient ingredient in dbAllIngredients)
                {
                    AllIngredientSelectList.Add(
                        new SelectListItem
                        {
                            Text = ingredient.IngredientName,
                            Value = ingredient.IngredientID.ToString(),
                        });
                }

                data.Ingredients = AllIngredientSelectList;

                return View("Update", data);
            }


            Pizza? pizzaToEdit = _myDb.Pizzas.Where(p => p.PizzaId == id).Include(p => p.Ingredients).FirstOrDefault();

            if (pizzaToEdit != null)
            {
                pizzaToEdit.Ingredients.Clear();

                pizzaToEdit.Name = data.Pizza.Name;
                pizzaToEdit.Description = data.Pizza.Description;
                pizzaToEdit.ImgPath = data.Pizza.ImgPath;
                pizzaToEdit.Price = data.Pizza.Price;
                pizzaToEdit.CategoryId = data.Pizza.CategoryId;

                if (data.SelectedIngredientsId != null)
                {
                    foreach (string selectedIngredientId in data.SelectedIngredientsId)
                    {
                        int intSelectedIngredientId = int.Parse(selectedIngredientId);

                        Ingredient? ingredientInDb = _myDb.Ingredients.Where(i => i.IngredientID == intSelectedIngredientId).FirstOrDefault();

                        if (ingredientInDb != null)
                        {
                            pizzaToEdit.Ingredients.Add(ingredientInDb);
                        }
                    }
                }

                _myDb.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogWarning("Pizza not found for update. ID: {id}", id);
                return NotFound("The pizza you want to edit was not found");
            }

        }

        [Authorize(Roles = "ADMIN")]
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
