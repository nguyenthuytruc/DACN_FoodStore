using Microsoft.AspNetCore.Mvc;
using FoodStore.Models;
using Microsoft.EntityFrameworkCore; // Thêm không gian tên này
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using FoodStore.Repositories; // Thêm không gian tên này nếu chưa có

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
            Console.WriteLine($"Is valid: {ModelState.IsValid}");
            _logger.LogInformation($"Is valid: {ModelState.IsValid}");

            //if (ModelState.IsValid)
            //{
                var newIngredient = new Ingredients
                {
                    // Do not set Id here; let it auto-generate
                    Name = ing.Name,
                    Image = ing.Image,
                    Unit = ing.Unit,
                    Quantity = ing.Quantity,
                    FoodId = ing.FoodId,
                    IsDeleted = false
                };

                await _ingredientRepository.AddAsync(newIngredient); // Use newIngredient here
                await _context.SaveChangesAsync(); // Save changes to the database

                return RedirectToAction("Index"); // Redirect to Index after adding the ingredient
            //}

            //// If the model is invalid, return the view with the errors
            //ViewBag.FoodList = _context.Foods.Where(f => !f.IsDeleted).ToList();
            //return View(ing);
        }


        // [HttpPost]
        // public async Task<IActionResult> Add(IngredientViewModel model)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         var newIngredient = new Ingredients
        //         {
        //             Name = model.Ingredient.Name,
        //             Image = model.Ingredient.Image,
        //             Unit = model.Ingredient.Unit,
        //             Quantity = model.Ingredient.Quantity,
        //             FoodId = model.Ingredient.FoodId,
        //             IsDeleted = false
        //         };

        //         _context.Ingredients.Add(newIngredient);
        //         await _context.SaveChangesAsync();

        //         return RedirectToAction("Index"); // Refreshes the index page with new data
        //     }

        //     // If model is invalid, reload page with errors
        //     model.Foods = await _context.Foods.ToListAsync();
        //     return View(model);
        // }



        // GET: Admin/Ingredient/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id); // Sử dụng FindAsync
            if (ingredient == null || ingredient.IsDeleted)
            {
                return NotFound();
            }

            // Lấy danh sách món ăn để điền vào dropdown trong view Edit
            ViewBag.FoodList = _context.Foods.Where(f => !f.IsDeleted).ToList();
            return View(ingredient);
        }

        // POST: Admin/Ingredient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ingredients ingredient)
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
                    existingIngredient.FoodId = ingredient.FoodId; // Sửa từ FoodCategoryId thành FoodId
                    existingIngredient.Quantity = ingredient.Quantity;
                    existingIngredient.Unit = ingredient.Unit;

                    _context.Update(existingIngredient);
                    await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }

            // Cập nhật lại danh sách món ăn nếu có lỗi
            ViewBag.FoodList = _context.Foods.Where(f => !f.IsDeleted).ToList();
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
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingredient = await _context.Ingredients.FindAsync(id); // Sử dụng FindAsync
            if (ingredient != null)
            {
                ingredient.IsDeleted = true; // Đánh dấu là đã xóa
                _context.Update(ingredient);
                await _context.SaveChangesAsync(); // Sử dụng SaveChangesAsync
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
