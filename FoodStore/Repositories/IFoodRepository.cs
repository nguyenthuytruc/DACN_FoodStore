using FoodStore.Models;
namespace FoodStore.Repositories
{
    public interface IFoodRepository
    {
        Task<IEnumerable<Food>> GetAllAsync();
        Task<Food> GetByIdAsync(int id);
        Task AddAsync(Food food);
        Task UpdateAsync(Food food);
        Task DeleteAsync(int id);
    }
}
