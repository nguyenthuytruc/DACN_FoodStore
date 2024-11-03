using Microsoft.AspNetCore.Mvc;
using FoodStore.Models;
using Microsoft.EntityFrameworkCore; // Thêm không gian tên này
using Microsoft.Extensions.Logging;
using FoodStore.Repositories; // Thêm không gian tên này nếu chưa có
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class IngredientController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<IngredientController> _logger;
        private readonly IIngredientRepository _ingredientRepository;

        public IngredientController(ApplicationDbContext context, ILogger<IngredientController> logger, IIngredientRepository ingredientRepository)
        {
            _context = context;
            _logger = logger;
            _ingredientRepository = ingredientRepository;
        }

        // GET: Admin/Ingredient
        public async Task<IActionResult> Index()
        {
            var ingredients = await _context.Ingredients
                .Where(i => !i.IsDeleted)
                .ToListAsync(); // Gọi ToListAsync trên IQueryable
            return View(ingredients);
        }

        public IActionResult Create()
        {
            _logger.LogInformation($"///////////// IN public IActionResult Create");
            Console.WriteLine($"cs///////////// IN public IActionResult Create");
            // Lấy danh sách món ăn từ cơ sở dữ liệu
            ViewBag.FoodList = _context.Foods.Where(f => !f.IsDeleted).ToList();
            return View("Add"); // Trả về view Add.cshtml
        }

        // POST: Admin/Ingredient/Create
        [HttpPost]
        public async Task<IActionResult> Add(Ingredients ing)
        {
            _logger.LogInformation("///////////// IN Task<IActionResult> Add(Ingredients ing)");
            _logger.LogInformation($"Is valid: {ModelState.IsValid}");

            if (ModelState.IsValid)
            {
                var newIngredient = new Ingredients
                {
                    Name = ing.Name,
                    Image = ing.Image,
                    Unit = ing.Unit,
                    Quantity = ing.Quantity,
                    IsDeleted = false
                };

                await _ingredientRepository.AddAsync(newIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // If model is invalid, add a custom error message
            ModelState.AddModelError(string.Empty, "Thông tin nhập vào không hợp lệ. Vui lòng kiểm tra lại.");
            ViewBag.FoodList = _context.Foods.Where(f => !f.IsDeleted).ToList();
            return View(ing);
        }

        // GET: Admin/Ingredient/Update/5
        public async Task<IActionResult> Update(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id); // Sử dụng FindAsync
            if (ingredient == null || ingredient.IsDeleted)
            {
                return NotFound();
            }

            // Lấy danh sách món ăn để điền vào dropdown trong view Edit
            ViewBag.FoodList = new SelectList(_context.Foods.Where(f => !f.IsDeleted), "Id", "Name");
            return View(ingredient);
        }

        // POST: Admin/Ingredient/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Ingredients ingredient)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingIngredient = await _context.Ingredients.FindAsync(id); // Sử dụng FindAsync
                if (existingIngredient != null)
                {
                    existingIngredient.Name = ingredient.Name;
                    existingIngredient.Image = ingredient.Image;
                    existingIngredient.Quantity = ingredient.Quantity;
                    existingIngredient.Unit = ingredient.Unit;

                    _context.Update(existingIngredient);
                    await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            // Cập nhật lại danh sách món ăn nếu có lỗi
            ViewBag.FoodList = new SelectList(_context.Foods.Where(f => !f.IsDeleted), "Id", "Name");
            return View(ingredient);
        }

        // GET: Admin/Ingredient/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id); // Sử dụng FindAsync
            if (ingredient == null || ingredient.IsDeleted)
            {
                return NotFound();
            }
            return View(ingredient);
        }

        // POST: Admin/Ingredient/Delete/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id); // Use FindAsync to get the ingredient
            if (ingredient != null && !ingredient.IsDeleted) // Check if ingredient exists and is not already deleted
            {
                ingredient.IsDeleted = true; // Mark the ingredient as deleted
                _context.Update(ingredient); // Update the entry in the context
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            else
            {
                _logger.LogWarning($"Ingredient with ID {id} not found or already deleted.");
            }

            return RedirectToAction("Index");
            //return RedirectToAction(nameof(Index)); // Redirect to Index
        }
    }
}
