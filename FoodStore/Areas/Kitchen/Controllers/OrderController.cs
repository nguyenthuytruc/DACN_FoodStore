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
                .Where(od => !od.Order.StatusPay && od.Status != 2)
                .ToList();

            return View(acceptedOrderDetails);
        }


        //[HttpGet]
        //public async Task<IActionResult> OrderAccepted()
        //{
        //    var orderDetails = await _orderRepository.GetAcceptedOrderDetails();

        //    // Lọc chỉ lấy những món đã xong
        //    var completedOrders = orderDetails.Where(od => od.Status == 2).ToList();
        //    return View(completedOrders); // Sửa ở đây
        //}

        public async Task<IActionResult> OrderAccepted()
        {
            var order = await _orderRepository.GetListOrderAccept();
            ViewBag.orderList = order;
            return View(order);
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

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDetailStatus(int orderId, int foodId, int status)
        {
            var orderDetail = await _context.OrderDetails
                .Include(od => od.Order) // Bao gồm thông tin đơn hàng
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.FoodId == foodId);

            if (orderDetail != null)
            {
                orderDetail.Status = status; // Cập nhật trạng thái món ăn

                // Kiểm tra nếu món ăn đã xong và đơn hàng đã thanh toán
                if (status == 2 && orderDetail.Order.StatusPay) // 2 là trạng thái "Đã xong"
                {
                    // Di chuyển món ăn vào danh sách "Đơn hàng đã đặt"
                    _context.OrderDetails.Remove(orderDetail); // Xóa món ăn khỏi danh sách đang chờ xử lý
                }

                await _context.SaveChangesAsync();

                //// Gửi thông báo tới tất cả client để cập nhật danh sách món ăn
                //await _hubContext.Clients.All.SendAsync("ReceiveOrderUpdate");
            }

            return RedirectToAction("Index"); // Hoặc chuyển tới một trang khác nếu cần
        }
    }
}
