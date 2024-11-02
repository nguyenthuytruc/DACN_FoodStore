using System.ComponentModel.DataAnnotations;

namespace FoodStore.Models
{
    public class Ingredients
    {
        [Key]
        public int Id { get; set; } // Khóa chính

        [Required(ErrorMessage = "Tên nguyên liệu là bắt buộc.")]
        [StringLength(255, ErrorMessage = "Tên nguyên liệu không được vượt quá 255 ký tự.")]
        public string Name { get; set; } // Tên nguyên liệu

        public string Image { get; set; } // Hình ảnh của nguyên liệu

        public int FoodId { get; set; } // Khóa ngoại tới bảng Foods

        [Required(ErrorMessage = "Số lượng còn lại là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn hoặc bằng 0.")]
        public int Quantity { get; set; } // Số lượng còn lại

        [Required(ErrorMessage = "Đơn vị là bắt buộc.")]
        [StringLength(50, ErrorMessage = "Đơn vị không được vượt quá 50 ký tự.")]
        public string Unit { get; set; } // Đơn vị của nguyên liệu

        public bool IsDeleted { get; set; } // Trạng thái xóa
    }
}
