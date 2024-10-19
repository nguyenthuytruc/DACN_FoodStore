using FoodStore.Models;
using FoodStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {

        private readonly IOrderRepository _orderRepository;
        private readonly ITableRepository _tableRepository;
        public OrderController(IOrderRepository orderRepository, ITableRepository tableRepository)
        {
            _orderRepository = orderRepository;
            _tableRepository = tableRepository;
        }
        [HttpGet]

        //chỗ này cần sửa lại 

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
        [Route("/Admin/Order/Detail/{id:int}")]
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
        /*[HttpGet]
        public async Task<IActionResult> GetListOrderAccept(int id)
        {
            var order = await _orderRepository.GetListOrderAccept(id);
            ViewBag.IdStore = 1;
            return View(order);
        }*/
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
    }
}
