using System.Collections.Generic;
using System.Linq; // Thêm vào để sử dụng LINQ
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodStore.Models;
using FoodStore.Repositories;

namespace FoodStore.Areas.Kitchen.Controllers
{
    [Area("Kitchen")]
    [Authorize(Roles = SD.Role_Kitchen)]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderRepository _orderRepository;
        private readonly ITableRepository _tableRepository;

        public OrderController(ApplicationDbContext context, IOrderRepository orderRepository, ITableRepository tableRepository)
        {
            _context = context;
            _orderRepository = orderRepository;
            _tableRepository = tableRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var acceptedOrderDetails = await _orderRepository.GetAcceptedOrderDetails();

            // Filter for items that are either unpaid or not yet completed
            acceptedOrderDetails = acceptedOrderDetails
                .Where(od => !od.Order.StatusPay && od.Status < 2)
                .ToList();

            return View(acceptedOrderDetails);
        }


        public async Task<IActionResult> OrderAccepted()
        {
            var acceptedOrderDetails = await _orderRepository.GetAcceptedOrderDetails();

            acceptedOrderDetails = acceptedOrderDetails
                .Where(od => !od.Order.StatusPay && od.Status == 2)
                .ToList();

            return View("OrderAccepted", acceptedOrderDetails);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllOrderDetails()
        {
            var orderDetails = await _context.OrderDetails
                .Include(od => od.Food)
                .Include(od => od.Order)
                .Where(od => !od.Order.StatusPay) // Lọc các đơn hàng chưa thanh toán
                .Where(od => od.Status != 2) // Lọc ra các món ăn chưa hoàn thành
                .OrderBy(od => od.Order.Created) // Sắp xếp theo thời gian tạo (bỏ qua sắp xếp theo bàn)
                .ToListAsync();

            return PartialView("_OrderDetailsQueue", orderDetails); // Trả về một partial view
        }

        public async Task<IActionResult> DetailAccepted(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            var orderDetail = await _orderRepository.GetListOrderDetailsByIdOrder(id);
            ViewBag.OrderDetails = orderDetail;

            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Id = order.Id;
            return View(order);
        }

        //[HttpPost]
        //public async Task<IActionResult> UpdateOrderDetailStatus(int orderId, int foodId, int status)
        //{
        //    var orderDetail = await _context.OrderDetails
        //        .Include(od => od.Order) // Bao gồm thông tin đơn hàng
        //        .FirstOrDefaultAsync(od => od.OrderId == orderId && od.FoodId == foodId);

        //    if (orderDetail != null){
        //        orderDetail.Status = status; // Cập nhật trạng thái món ăn

        //        await _context.SaveChangesAsync();
        //    }

        //    return RedirectToAction("Index"); // Hoặc chuyển tới một trang khác nếu cần
        //}

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDetailStatus(int orderId, int foodId, int status)
        {
            // Debug: In ra thông tin đầu vào
            Console.WriteLine($"Order ID: {orderId}, Food ID: {foodId}, Status: {status}");

            // Lấy chi tiết đơn hàng
            var orderDetail = await _context.OrderDetails
                .Include(od => od.Order)
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.FoodId == foodId);

            // Debug: Kiểm tra nếu không tìm thấy chi tiết đơn hàng
            if (orderDetail == null)
            {
                Console.WriteLine("Order detail not found");
                return NotFound("Order detail not found");
            }
            Console.WriteLine($"Found order detail for Order ID: {orderId} with Food ID: {foodId}");

            // Lấy danh sách công thức của món ăn (nguyên liệu cần thiết)
            var formulaList = await _context.FoodIngredient
                .Where(fi => fi.FoodId == foodId)
                .ToListAsync();

            // Debug: In ra số lượng nguyên liệu trong công thức
            Console.WriteLine($"Number of ingredients in formula for Food ID {foodId}: {formulaList.Count}");

            // Duyệt qua từng nguyên liệu trong công thức và cập nhật số lượng còn lại trong kho
            foreach (var formula in formulaList)
            {
                // Lấy nguyên liệu trong kho
                var ingredient = await _context.Ingredients
                    .FirstOrDefaultAsync(i => i.Id == formula.IngredientId);

                // Debug: Kiểm tra nếu không tìm thấy nguyên liệu trong kho
                if (ingredient == null)
                {
                    Console.WriteLine($"Ingredient with ID {formula.IngredientId} not found in inventory.");
                    continue;
                }

                Console.WriteLine($"Ingredient ID {ingredient.Id} - Current Quantity: {ingredient.Quantity}");

                // Trừ số lượng theo công thức
                int quantityRequired = formula.QuantityRequired; // Giả sử có thuộc tính này trong công thức
                ingredient.Quantity -= quantityRequired;

                // Debug: In ra số lượng sau khi trừ
                Console.WriteLine($"Updated Quantity for Ingredient ID {ingredient.Id}: {ingredient.Quantity}");

                // Kiểm tra nếu số lượng còn lại nhỏ hơn 0
                if (ingredient.Quantity < 0)
                {
                    ingredient.Quantity = 0; // Không cho phép số lượng âm
                    Console.WriteLine($"Quantity for Ingredient ID {ingredient.Id} set to 0 to avoid negative value");
                }

                // Cập nhật lại nguyên liệu
                _context.Ingredients.Update(ingredient);
            }

            // Cập nhật trạng thái của món ăn trong chi tiết đơn hàng
            Console.WriteLine($"Updating status for Order Detail - Order ID: {orderId}, Food ID: {foodId} to Status: {status}");
            orderDetail.Status = status;

            // Lưu thay đổi
            await _context.SaveChangesAsync();
            Console.WriteLine("Changes saved to database");

            return RedirectToAction("Index"); // Hoặc điều hướng tới trang khác nếu cần
        }


        [HttpPost]
        public async Task<IActionResult> MoveToOngoing(int orderId, int foodId, int status)
        {
            var orderDetail = await _context.OrderDetails
                .Include(od => od.Order) // Bao gồm thông tin đơn hàng
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.FoodId == foodId);

            bool notYetDelivered = orderDetail.Status < 3;
            if (orderDetail != null && notYetDelivered)
            {
                orderDetail.Status = status; // Cập nhật trạng thái món ăn
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("OrderAccepted"); // Hoặc chuyển tới một trang khác nếu cần
        }
    }
}
