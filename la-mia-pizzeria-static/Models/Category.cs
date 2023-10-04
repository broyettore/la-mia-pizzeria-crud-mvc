using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_static.Models
{
    public class Category
    {

        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Title can not be empty")]
        [StringLength(80, ErrorMessage = "Title can not go over 80 characters")]
        public string Title { get; set; }
        public List<Pizza>? Pizzas { get; set; }
        public Category() { }
    }
}
