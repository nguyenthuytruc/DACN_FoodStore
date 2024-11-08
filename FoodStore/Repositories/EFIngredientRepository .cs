using FoodStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodStore.Repositories
{
    public class EFIngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        public EFIngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ingredients>> GetAllAsync()
        {
            // Lấy tất cả nguyên liệu chưa bị xóa
            return await _context.Ingredients.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Ingredients> GetByIdAsync(int id)
        {
            // Lấy nguyên liệu theo ID nếu chưa bị xóa
            return await _context.Ingredients.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task AddAsync(Ingredients ingredient)
        {
            // Thêm nguyên liệu mới vào cơ sở dữ liệu
            _context.Ingredients.Add(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Ingredients ingredient)
        {
            // Cập nhật nguyên liệu
            _context.Ingredients.Update(ingredient);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // Tìm nguyên liệu theo ID
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient != null)
            {
                // Đánh dấu nguyên liệu là đã xóa
                ingredient.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Ingredients>> GetAllIngredientsAsync()
        {
            // Truy vấn lấy danh sách nguyên liệu chưa bị xóa
            var ingredients = await _context.Ingredients
                                            .Where(i => !i.IsDeleted)
                                            .ToListAsync();

            return ingredients;
        }

        public async Task AddFoodIngredientAsync(FoodIngredient foodIngredient)
        {
            _context.FoodIngredient.Add(foodIngredient);
            await _context.SaveChangesAsync();
        }

    }
}
