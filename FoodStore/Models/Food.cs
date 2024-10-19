using System.ComponentModel.DataAnnotations;

namespace FoodStore.Models
{
    public class Food : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
        public string Image { get; set; }
        public string Type { get; set; } //Đơn vị tính
        public decimal Price { get; set; }
        public int Status { get; set; } = 0;
        public int FoodCategoryId { get; set; }
        public FoodCategory FoodCategorys { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
