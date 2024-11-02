using System.ComponentModel.DataAnnotations;

namespace FoodStore.Models
{
    public class Ingredients
    {
        [Key]
        public int Id { get; set; } // Primary key

        [Required(ErrorMessage = "Tên nguyên liệu là bắt buộc.")]
        [StringLength(255, ErrorMessage = "Tên nguyên liệu không được vượt quá 255 ký tự.")]
        public string Name { get; set; } // Required

        public string? Image { get; set; } // Nullable - image of ingredient

        public int? FoodId { get; set; } // Nullable - foreign key to Foods table

        [Required(ErrorMessage = "Số lượng còn lại là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0.")]
        public int Quantity { get; set; } // Required

        [Required(ErrorMessage = "Đơn vị là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Đơn vị không được vượt quá 50 ký tự.")]
        public string Unit { get; set; } // Required

        public bool IsDeleted { get; set; } // Non-nullable, as it represents status
    }
}
