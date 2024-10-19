using FoodStore.Models;
using FoodStore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.Controllers
{
    public class FoodCategoryController : Controller
    {

        private readonly IFoodRepository _foodRepository;
        private readonly IFoodCategoryRepository _foodcategoryRepository;
        public FoodCategoryController(IFoodRepository foodRepository, IFoodCategoryRepository
        foodcategoryRepository)
        {
            _foodRepository = foodRepository;
            _foodcategoryRepository = foodcategoryRepository;
        }
        public async Task<IActionResult> Index()
        {
            var foodCategories = await _foodcategoryRepository.GetAllAsync();
            return View(foodCategories);
        }
        public async Task<IActionResult> Display(int id)
        {
            var foodcategory = await _foodcategoryRepository.GetByIdAsync(id);
            if (foodcategory == null)
            {
                return NotFound();
            }
            return View(foodcategory);
        }
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(FoodCategory foodcategory)
        {
            if (ModelState.IsValid)
            {
                _foodcategoryRepository.AddAsync(foodcategory);
                return RedirectToAction(nameof(Index));
            }
            return View(foodcategory);
        }
        public async Task<IActionResult> Update(int id)
        {
            var foodcategory = await _foodcategoryRepository.GetByIdAsync(id);
            if (foodcategory == null)
            {
                return NotFound();
            }
            return View(foodcategory);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, FoodCategory foodcategory)
        {
            if (id != foodcategory.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _foodcategoryRepository.UpdateAsync(foodcategory);
                return RedirectToAction(nameof(Index));
            }
            return View(foodcategory);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var foodcategory = await _foodcategoryRepository.GetByIdAsync(id);
            if (foodcategory == null)
            {
                return NotFound();
            }
            return View(foodcategory);
        }


        [HttpPost, ActionName("DeleteConfirmed")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await _foodcategoryRepository.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
