namespace la_mia_pizzeria_static.Models
{
    public class Ingredient
    {
        public int IngredientID { get; set; }
        public string IngredientName { get; set; }

        public List<Pizza> Pizzas { get; set;}

        public Ingredient() { }
    }
}
