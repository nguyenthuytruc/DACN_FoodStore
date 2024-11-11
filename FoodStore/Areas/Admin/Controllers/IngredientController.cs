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
        [HttpPost]
        public async Task<IActionResult> Add(Ingredients ing, IFormFile Image)
        {
            _logger.LogInformation("In Task<IActionResult> Add(Ingredients ing)");
            _logger.LogInformation($"Is valid: {ModelState.IsValid}");

            // Exclude 'Image' from model validation
            ModelState.Remove("Image");

            if (ModelState.IsValid)
            {
                string imagePath = null;

                if (Image != null && Image.Length > 0)
                {
                    // Define the path to save the uploaded file
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await Image.CopyToAsync(fileStream);
                    }

                    imagePath = "images/" + uniqueFileName; // Relative path to save in the database
                }

                var newIngredient = new Ingredients
                {
                    Name = ing.Name,
                    Image = imagePath,
                    Unit = ing.Unit,
                    Quantity = ing.Quantity,
                    IsDeleted = false
                };

                await _ingredientRepository.AddAsync(newIngredient);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(string.Empty, "Thông tin nhập vào không hợp lệ. Vui lòng kiểm tra lại.");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Ingredients ingredient, IFormFile Image)
        {
            if (id != ingredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingIngredient = await _context.Ingredients.FindAsync(id);
                if (existingIngredient != null)
                {
                    existingIngredient.Name = ingredient.Name;
                    existingIngredient.Quantity = ingredient.Quantity;
                    existingIngredient.Unit = ingredient.Unit;

                    // Check if a new image file was uploaded
                    if (Image != null && Image.Length > 0)
                    {
                        // Define the path to save the uploaded file
                        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                        Directory.CreateDirectory(uploadsFolder); // Ensure the directory exists

                        var uniqueFileName = Guid.NewGuid().ToString() + "_" + Image.FileName;
                        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(fileStream);
                        }

                        existingIngredient.Image = "images/" + uniqueFileName; // Save the relative path in the database
                    }

                    _context.Update(existingIngredient);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            // Load dropdowns or other data for re-rendering the form on error
            ViewBag.FoodList = new SelectList(_context.Foods.Where(f => !f.IsDeleted), "Id", "Name");
            return View(ingredient);
        }
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
