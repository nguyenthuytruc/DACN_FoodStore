using FoodStore.Models;
using FoodStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            var order = await _orderRepository.GetListOrder();
            ViewBag.orderList = order;
            return View(order);
        }

        public async Task<IActionResult> OrderAccepted()
        {
            var order = await _orderRepository.GetListOrderAccept();
            ViewBag.orderList = order;
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> Accept(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            var orderupdate = await _orderRepository.UpdateAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Kitchen/Order/Detail/{id:int}")]
        public async Task<IActionResult> Detail(int id)
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

        [HttpGet]
        public async Task<IActionResult> Denied(int id)
        {
            await _orderRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }

        // Handle form submit để update trạng thái món ăn
        // [HttpPost]
        // public async Task<IActionResult> UpdateOrderDetailsStatus(Dictionary<string, bool> Statuses, int orderId)
        // {
        //     foreach (var item in Statuses)
        //     {
        //         // Tách OrderId và FoodId từ key
        //         var keys = item.Key.Split(',');
        //         int orderDetailOrderId = int.Parse(keys[0]);
        //         int orderDetailFoodId = int.Parse(keys[1]);

        //         bool isChecked = item.Value;

        //         // Tìm OrderDetail dựa trên OrderId và FoodId
        //         var orderDetail = await _context.OrderDetails
        //             .FirstOrDefaultAsync(od => od.OrderId == orderDetailOrderId && od.FoodId == orderDetailFoodId);

        //         if (orderDetail != null)
        //         {
        //             // Cập nhật trạng thái dựa trên checkbox
        //             orderDetail.Status = isChecked ? 1 : 0;
        //         }
        //     }

        //     // Lưu các thay đổi
        //     await _context.SaveChangesAsync();

        //     // Lấy lại chi tiết đơn hàng và trả về View DetailAccepted
        //     var order = await _orderRepository.GetOrderById(orderId);
        //     var orderDetails = await _orderRepository.GetListOrderDetailsByIdOrder(orderId);
        //     ViewBag.OrderDetails = orderDetails;

        //     return View("DetailAccepted", order); // Trả về lại view DetailAccepted
        // }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderDetailsStatus(Dictionary<string, bool> Statuses, int orderId)
        {
            foreach (var item in Statuses)
            {
                // Tách OrderId và FoodId từ key
                var keys = item.Key.Split(',');
                int orderDetailOrderId = int.Parse(keys[0]);
                int orderDetailFoodId = int.Parse(keys[1]);

                bool isChecked = item.Value;

                // Tìm OrderDetail dựa trên OrderId và FoodId
                var orderDetail = await _context.OrderDetails
                    .FirstOrDefaultAsync(od => od.OrderId == orderDetailOrderId && od.FoodId == orderDetailFoodId);

                if (orderDetail != null)
                {
                    // Cập nhật trạng thái dựa trên checkbox
                    orderDetail.Status = isChecked ? 1 : 0;
                }
            }

            // Lưu các thay đổi
            await _context.SaveChangesAsync();

            // Lấy lại chi tiết đơn hàng và trả về View DetailAccepted
            var order = await _orderRepository.GetOrderById(orderId);
            var orderDetails = await _orderRepository.GetListOrderDetailsByIdOrder(orderId);
            ViewBag.OrderDetails = orderDetails;

            return View("DetailAccepted", order); // Trả về lại view DetailAccepted
        }

    }
}
