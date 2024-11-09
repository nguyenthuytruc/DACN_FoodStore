using FoodStore.Models;
using FoodStore.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FoodStore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class FoodController : Controller
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IFoodCategoryRepository _foodcategoryRepository;
        private readonly ApplicationDbContext _context;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IFoodIngredientRepository _foodIngredientRepository;

        public FoodController(
            IFoodRepository foodRepository, 
            IFoodCategoryRepository foodcategoryRepository,
            ApplicationDbContext context,
            IIngredientRepository ingredientRepository,
            IFoodIngredientRepository foodIngredientRepository
        )

        {
            _foodRepository = foodRepository;
            _foodcategoryRepository = foodcategoryRepository;
            _context = context;
            _ingredientRepository = ingredientRepository;
            _foodIngredientRepository = foodIngredientRepository;
        }

      
        public async Task<IActionResult> Index()
        {
            var foods = await _foodRepository.GetAllAsync();
            return View(foods);
        }
       
        public async Task<IActionResult> Add()
        {
            var foodcategories = await _foodcategoryRepository.GetAllAsync();
            ViewBag.FoodCategories = new SelectList(foodcategories, "Id", "Name");
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Add(Food food, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                if (Image != null)
                {
                    
                    food.Image = await SaveImage(Image);
                }
                await _foodRepository.AddAsync(food);
                return RedirectToAction(nameof(Index));
            }
           
            var foodcategories = await _foodcategoryRepository.GetAllAsync();
            ViewBag.FoodCategories = new SelectList(foodcategories, "Id", "Name");
            return View(food);
        }

        
        private async Task<string> SaveImage(IFormFile image)
        {
            var savePath = Path.Combine("wwwroot/images", image.FileName); //

            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            return "/images/" + image.FileName;
        }

        public async Task<IActionResult> Display(int id)
        {
            var food = await _foodRepository.GetByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            // Lấy danh sách nguyên liệu thuộc về food có Id = id
            var foodIngredients = await _foodIngredientRepository.GetIngredientsByFoodIdAsync(id);
            var allIngredients = await _ingredientRepository.GetAllIngredientsAsync();

            // Ghi log để kiểm tra danh sách lấy được
            Console.WriteLine($"Food ID: {id}");
            Console.WriteLine($"Food Ingredients Count: {foodIngredients?.Count ?? 0}");
            Console.WriteLine("Food Ingredients:");
            foreach (var ing in foodIngredients)
            {
                Console.WriteLine($" - FoodId ID: {ing.FoodId}, IngredientId: {ing.IngredientId}");
            }

            Console.WriteLine("All Ingredients:");
            foreach (var ing in allIngredients)
            {
                Console.WriteLine($" - Ingredient ID: {ing.Id}, Name: {ing.Name}");
            }

            // Gán cả hai danh sách vào ViewBag
            ViewBag.FoodIngredients = foodIngredients; // Nguyên liệu của món ăn
            ViewBag.AllIngredients = allIngredients;   // Tất cả nguyên liệu

            return View(food);
        }



        // Hiển thị form cập nhật sản phẩm
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var food = await _foodRepository.GetByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            var foodcategories = await _foodcategoryRepository.GetAllAsync();
            ViewBag.FoodCategories = new SelectList(foodcategories, "Id", "Name", food.FoodCategoryId);

            return View(food);
        }

        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Food food, IFormFile imageUrl)

        {
            ModelState.Remove("ImageUrl"); // Loại bỏ xác thực ModelState cho ImageUrl

            if (id != food.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var existingFood= await

                _foodRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                                                     // Giữ nguyên thông tin hình ảnh nếu không có hình mới được tải lên

                if (imageUrl == null)
                {
                    food.Image = existingFood.Image;
                }
                else
                {
                    // Lưu hình ảnh mới
                    food.Image = await SaveImage(imageUrl);
                }
                // Cập nhật các thông tin khác của sản phẩm
                existingFood.Name = food.Name;
                existingFood.Price = food.Price;
                existingFood.FoodCategoryId = food.FoodCategoryId;
                existingFood.Image = food.Image;
                existingFood.Status = food.Status;
                existingFood.Type = food.Type;
                await _foodRepository.UpdateAsync(existingFood);
                return RedirectToAction(nameof(Index));
            }
            var foodcategories = await _foodcategoryRepository.GetAllAsync();
            ViewBag.FoodCategories = new SelectList(foodcategories, "Id", "Name");
            return View(food);
        }
        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var food = await _foodRepository.GetByIdAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _foodRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public async Task<IActionResult> SaveRecipe(int id, List<FoodIngredient> ingredients)
        //{
        //    foreach (var ingredient in ingredients)
        //    {
        //        if (ingredient.IngredientId != 0) // Bỏ qua các hàng trống
        //        {
        //            ingredient.FoodId = id;
        //            await _ingredientRepository.AddFoodIngredientAsync(ingredient);
        //        }
        //    }

        //    return RedirectToAction("Detail", new { id });
        //}

        [HttpPost]
        public async Task<IActionResult> SaveRecipe(int id, List<FoodIngredient> ingredients)
        {
            Console.WriteLine($"Starting SaveRecipe for Food ID: {id} with {ingredients.Count} ingredients.");

            // Log each ingredient in the list before processing
            foreach (var ing in ingredients)
            {
                Console.WriteLine($"Received Ingredient - ID: {ing.IngredientId}, QuantityRequired: {ing.QuantityRequired}");
            }

            var food = await _foodRepository.GetByIdAsync(id);
            if (food == null)
            {
                Console.WriteLine("Food not found.");
                return NotFound("Food not found.");
            }

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    // Step 1: Delete existing ingredients
                    var existingIngredients = await _foodIngredientRepository.GetIngredientsByFoodIdAsync(id);
                    Console.WriteLine($"Found {existingIngredients.Count} existing ingredients to delete for Food ID: {id}.");

                    foreach (var ingredient in existingIngredients)
                    {
                        Console.WriteLine($"Deleting Ingredient - FoodID: {ingredient.FoodId}, IngredientID: {ingredient.IngredientId}");
                        _context.FoodIngredient.Remove(ingredient);
                    }
                    await _context.SaveChangesAsync();
                    Console.WriteLine("Existing ingredients deleted successfully.");

                    // Step 2: Add new ingredients
                    foreach (var ingredient in ingredients)
                    {
                        var newIngredient = new FoodIngredient
                        {
                            FoodId = id,
                            IngredientId = ingredient.IngredientId,
                            QuantityRequired = ingredient.QuantityRequired
                        };

                        Console.WriteLine($"Adding new Ingredient - FoodID: {newIngredient.FoodId}, IngredientID: {newIngredient.IngredientId}, QuantityRequired: {newIngredient.QuantityRequired}");
                        await _foodIngredientRepository.AddAsync(newIngredient);
                    }
                    Console.WriteLine("New ingredients added successfully.");

                    // Commit transaction
                    await transaction.CommitAsync();
                    Console.WriteLine("Transaction committed successfully.");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine($"Error occurred: {ex.Message}");
                    return StatusCode(500, "An error occurred while saving the recipe.");
                }
            }

            Console.WriteLine($"SaveRecipe completed for Food ID: {id}");
            return RedirectToAction("Display", new { id });
        }



    }
}
