using FoodStore.Models;
using FoodStore.Repositories;
using FoodStore.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace FoodStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITableRepository _tableRepository;
        private readonly ApplicationDbContext _context;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly OrderService _orderService;

        public OrderController(IOrderRepository orderRepository,
                               ITableRepository tableRepository,
                               ApplicationDbContext context,
                               IIngredientRepository ingredientRepository,
                               OrderService orderService
                               )
        {
            _orderRepository = orderRepository;
            _tableRepository = tableRepository;
            _ingredientRepository = ingredientRepository;
            _context = context;
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var order = await _orderRepository.GetListOrder();
            ViewBag.orderList = order;
            return View(order);
        }

        [HttpGet]
        public async Task<IActionResult> OrderAccepted()
        {
            var order = await _orderRepository.GetListOrderAccept();
            ViewBag.orderList = order;
            return View(order);
        }

        //[HttpGet]
        //public async Task<IActionResult> Accept(int id)
        //{
        //    var order = await _orderRepository.GetOrderById(id);
        //    await _orderRepository.UpdateAsync(id);  // Cập nhật trạng thái order
        //    return RedirectToAction("Index");
        //}

        //[HttpGet]
        //public async Task<IActionResult> Accept(int id)
        //{
        //    var canAcceptOrder = await _orderService.CanAcceptOrderAsync(id);
        //    if (!canAcceptOrder)
        //    {
        //        Console.WriteLine("Not enough ingredients to accept the order.");
        //        return BadRequest("Không đủ nguyên liệu để chấp nhận đơn hàng.");
        //    }

        //    await _orderRepository.UpdateAsync(id); // Cập nhật trạng thái đơn hàng
        //    return RedirectToAction("Index");
        //}

        [HttpGet]
        public async Task<IActionResult> Accept(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return RedirectToAction("Index");
            }

            var orderDetails = await _context.OrderDetails
                                              .Where(od => od.OrderId == id)
                                              .ToListAsync();

            var totalIngredientsNeeded = new Dictionary<int, int>();
            foreach (var orderDetail in orderDetails)
            {
                var foodIngredients = await _context.FoodIngredient
                                                    .Where(fi => fi.FoodId == orderDetail.FoodId)
                                                    .ToListAsync();

                foreach (var foodIngredient in foodIngredients)
                {
                    if (totalIngredientsNeeded.ContainsKey(foodIngredient.IngredientId))
                    {
                        totalIngredientsNeeded[foodIngredient.IngredientId] += foodIngredient.QuantityRequired * orderDetail.Quantity;
                    }
                    else
                    {
                        totalIngredientsNeeded[foodIngredient.IngredientId] = foodIngredient.QuantityRequired * orderDetail.Quantity;
                    }
                }
            }

            var insufficientIngredients = new List<string>();
            foreach (var ingredientId in totalIngredientsNeeded.Keys)
            {
                var ingredient = await _context.Ingredients.FindAsync(ingredientId);
                var requiredAmount = totalIngredientsNeeded[ingredientId];
                var availableAmount = ingredient?.Quantity ?? 0;

                if (ingredient == null || availableAmount < requiredAmount)
                {
                    var shortage = requiredAmount - availableAmount;
                    insufficientIngredients.Add($"Nguyên liệu ID {ingredientId} không đủ. Cần thêm: {shortage}. Có sẵn: {availableAmount}, Cần: {requiredAmount}");
                }
            }

            if (insufficientIngredients.Any())
            {
                var errorMsg = string.Join("<br/>", insufficientIngredients);
                ViewBag.ErrorMessage = errorMsg;  // TODO use view bag or similar to show message on GUI
                Console.WriteLine("ErrorMessage set in ViewBag: " + ViewBag.ErrorMessage);
                return BadRequest(errorMsg);
                // return RedirectToAction("Index");
            }

            order.Status = true;
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();

            ViewBag.SuccessMessage = "Order accepted successfully!";

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("/Admin/Order/Detail/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            // Lấy thông tin đơn hàng
            var order = await _orderRepository.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            // Lấy chi tiết đơn hàng
            var orderDetail = await _orderRepository.GetListOrderDetailsByIdOrder(id);
            ViewBag.OrderDetails = orderDetail;

            // Lấy danh sách nguyên liệu
            var ingredients = await _ingredientRepository.GetAllIngredientsAsync();

            // Kiểm tra số lượng nguyên liệu đã lấy
            Console.WriteLine($"Ingredients count: {ingredients.Count()}"); // Kiểm tra số lượng nguyên liệu
            ViewBag.Ingredients = ingredients;

            ViewBag.Id = order.Id;
            return View(order);
        }


        [HttpPost]
        public async Task<IActionResult> SaveRecipe(int id, List<FoodIngredient> ingredients)
        {
            foreach (var ingredient in ingredients)
            {
                if (ingredient.IngredientId != 0) // Bỏ qua các hàng trống
                {
                    ingredient.FoodId = id;
                    await _ingredientRepository.AddFoodIngredientAsync(ingredient);
                }
            }

            return RedirectToAction("Detail", new { id });
        }


        [HttpGet]
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
