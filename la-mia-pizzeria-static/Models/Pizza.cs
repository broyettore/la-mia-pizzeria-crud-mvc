using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace la_mia_pizzeria_static.Models
{
    [Table("Pizzas")]
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }
        public string Name { get; set; }

        public string ImgPath { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            string formattedPrice = "€" + this.Price.ToString();
            return $"\n Pizza Name: {this.Name} \n Description: {this.Description} \n Price: {formattedPrice}";
        }
    }
}
