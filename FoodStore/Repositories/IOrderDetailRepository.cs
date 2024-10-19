using FoodStore.Models;

namespace FoodStore.Repositories
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetAllAsync();
        Task<OrderDetail> GetByIdAsync(int id);
        Task AddAsync(OrderDetail order);
        Task UpdateAsync(OrderDetail order);
        Task DeleteAsync(int id);
    }
}
