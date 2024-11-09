using FoodStore.Models;
using Microsoft.EntityFrameworkCore;


public class EFOrderDetailRepository : IOrderDetailRepository
{
    private readonly ApplicationDbContext _context;

    public EFOrderDetailRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrderDetail>> GetAllAsync()
    {
        return await _context.OrderDetails.ToListAsync();
    }

    public async Task<OrderDetail> GetByIdAsync(int id)
    {
        return await _context.OrderDetails.FindAsync(id);
    }

    public async Task AddAsync(OrderDetail orderDetail)
    {
        _context.OrderDetails.Add(orderDetail);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderDetail orderDetail)
    {
        _context.OrderDetails.Update(orderDetail);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var orderdetail = await _context.OrderDetails.FindAsync(id);
        _context.OrderDetails.Remove(orderdetail);
        await _context.SaveChangesAsync();
    }

    // Đếm số món ăn chưa hoàn thành (giả sử Status = 0 là chưa hoàn thành)
    public async Task<int> CountUnfinishedAsync()
    {
        return await _context.OrderDetails.CountAsync(od => od.Status == 0);
    }

    // Đếm số món ăn chờ bàn giao (giả sử Status = 1 là chờ bàn giao)
    public async Task<int> CountWaitingForDeliveryAsync()
    {
        return await _context.OrderDetails.CountAsync(od => od.Status == 1);
    }
}
