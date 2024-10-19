using FoodStore.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.Repositories
{
    public class EFFoodRepository : IFoodRepository
    {
        private readonly ApplicationDbContext _context;
        public EFFoodRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Food>> GetAllAsync()
        {

            return await _context.Foods.Include("FoodCategorys").Where(x => x.IsDeleted == false).ToListAsync();

        }
        public async Task<Food> GetByIdAsync(int id)
        {
            return await _context.Foods.Include("FoodCategorys").FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddAsync(Food food)
        {
            _context.Foods.Add(food);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Food food)
        {
            _context.Foods.Update(food);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var food = await _context.Foods.FindAsync(id);
            food.IsDeleted = true;
            await _context.SaveChangesAsync();
        }
    }
}
