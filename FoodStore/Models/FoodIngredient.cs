using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodStore.Models
{
    public class FoodIngredient
    {
        [Key]
        public int Id { get; set; } // Khóa chính

        public int FoodId { get; set; }
        public int IngredientId { get; set; }

        public int QuantityRequired { get; set; }

        // Điều hướng đến Food
        [ForeignKey("FoodId")]
        public Food Food { get; set; }

        // Điều hướng đến Ingredient
        [ForeignKey("IngredientId")]
        public Ingredients Ingredients { get; set; }
    }
}
