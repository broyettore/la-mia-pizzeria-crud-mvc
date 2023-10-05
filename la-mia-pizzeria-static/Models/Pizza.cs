using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using la_mia_pizzeria_static.ValidationAttributes;

namespace la_mia_pizzeria_static.Models
{
    [Table("Pizzas")]
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }

        [Required(ErrorMessage = "You must insert a name")]
        [MaxLength(80, ErrorMessage = "Max length for a name is 80 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must insert a a valid path for your image")]
        public string ImgPath { get; set; }

        [Required(ErrorMessage = "You must insert a description of your pizza")]
        [MoreThanFiveWords]
        public string Description { get; set; }

        [Required(ErrorMessage = "You must insert a price for your pizza")]
        [PositivePrice]
        public double Price { get; set; }

        // 1:N relation with category
        public int? CategoryId { get; set; }
        public Category? Category { get; set; }

        // N:N relation with ingredients
        public List<Ingredient>? Ingredients { get; set; }


        public Pizza() { }

        public override string ToString()
        {
            string formattedPrice = "€" + Price.ToString();
            return $"\n Pizza Name: {Name} \n Description: {Description} \n Price: {formattedPrice}";
        }
    }
}
