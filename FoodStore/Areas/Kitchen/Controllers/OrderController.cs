﻿using System.Collections.Generic;
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

            // Lọc các mục chưa thanh toán và chưa hoàn thành
            acceptedOrderDetails = acceptedOrderDetails
                .Where(od => !od.Order.StatusPay && od.Status < 2)
                .OrderBy(od => od.Status)    // Sắp xếp theo Status tăng dần
                .ThenBy(od => od.FoodId)     // Sau đó sắp xếp theo FoodId
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
                .Where(od => od.Status < 2) // Lọc ra các món ăn chưa hoàn thành
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
            Console.WriteLine($"Order ID: {orderId}, Food ID: {foodId}, Status: {status}");

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var orderDetail = await GetOrderDetailAsync(orderId, foodId);
                    if (orderDetail == null)
                    {
                        Console.WriteLine("Order detail not found");
                        return NotFound("Order detail not found");
                    }

                    if (ShouldCheckAndDeductIngredients(orderDetail.Status, status))
                    {
                        var hasEnoughIngredients = await CheckAndUpdateIngredientsAsync(foodId);

                        // Nếu không đủ nguyên liệu, chuyển trạng thái thành -1
                        if (!hasEnoughIngredients)
                        {
                            Console.WriteLine("Not enough ingredients. Updating status to -1.");
                            UpdateOrderDetailStatus(orderDetail, -1);
                        }
                        else
                        {
                            Console.WriteLine("Inventory updated successfully for ingredients.");
                            UpdateOrderDetailStatus(orderDetail, status);
                        }
                    }
                    else
                    {
                        Console.WriteLine("No inventory update needed as the status change is not from 0 to 1.");
                        UpdateOrderDetailStatus(orderDetail, status);
                    }

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    Console.WriteLine("Changes saved to database");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    return StatusCode(500, "Error updating order details.");
                }
            }

            return RedirectToAction("Index");
        }

        // Hàm lấy chi tiết đơn hàng
        private async Task<OrderDetail> GetOrderDetailAsync(int orderId, int foodId)
        {
            return await _context.OrderDetails
                .Include(od => od.Order)
                .FirstOrDefaultAsync(od => od.OrderId == orderId && od.FoodId == foodId);
        }

        // Hàm kiểm tra nếu cần trừ nguyên liệu
        private bool ShouldCheckAndDeductIngredients(int currentStatus, int newStatus)
        {
            return currentStatus == 0 && newStatus == 1;
        }

        // Hàm kiểm tra và cập nhật nguyên liệu
        private async Task<bool> CheckAndUpdateIngredientsAsync(int foodId)
        {
            var formulaList = await _context.FoodIngredient
                .Where(fi => fi.FoodId == foodId)
                .ToListAsync();

            foreach (var formula in formulaList)
            {
                var ingredient = await _context.Ingredients
                    .FirstOrDefaultAsync(i => i.Id == formula.IngredientId);

                if (ingredient == null)
                {
                    Console.WriteLine($"Ingredient with ID {formula.IngredientId} not found in inventory.");
                    continue;
                }

                // Kiểm tra nếu không đủ số lượng
                if (ingredient.Quantity < formula.QuantityRequired)
                {
                    Console.WriteLine($"Not enough ingredient ID {ingredient.Id}. Required: {formula.QuantityRequired}, Available: {ingredient.Quantity}");
                    return false; // Thiếu nguyên liệu
                }
            }

            // Nếu đủ nguyên liệu, tiến hành trừ
            foreach (var formula in formulaList)
            {
                var ingredient = await _context.Ingredients
                    .FirstOrDefaultAsync(i => i.Id == formula.IngredientId);

                ingredient.Quantity -= formula.QuantityRequired;
                _context.Ingredients.Update(ingredient);
            }

            return true;
        }

        // Hàm cập nhật trạng thái của chi tiết đơn hàng
        private void UpdateOrderDetailStatus(OrderDetail orderDetail, int newStatus)
        {
            Console.WriteLine($"Updating status for Order Detail - Order ID: {orderDetail.OrderId}, Food ID: {orderDetail.FoodId} to Status: {newStatus}");
            orderDetail.Status = newStatus;
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
