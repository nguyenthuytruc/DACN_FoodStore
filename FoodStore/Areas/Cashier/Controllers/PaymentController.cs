using FoodStore.Models;
using FoodStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.Areas.Cashier.Controllers
{
    [Area("Cashier")]
    [Authorize(Roles = SD.Role_Cashier)]
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _htttoanRepository;

        public PaymentController(IPaymentRepository htttoanRepository)
        {
            _htttoanRepository = htttoanRepository;

        }


        public async Task<IActionResult> Index()
        {
            var htttoan = await _htttoanRepository.GetAllAsync();
            return View(htttoan);
        }
        
        public async Task<IActionResult> Display(int id)
        {
            var htttoan = await _htttoanRepository.GetByIdAsync(id);
            if (htttoan == null)
            {
                return NotFound();
            }
            return View(htttoan);
        }
        

    }
}
