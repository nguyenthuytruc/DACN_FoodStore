using Microsoft.AspNetCore.Mvc;
using FoodStore.Models;
using FoodStore.Repositories;


namespace FoodStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;

        public TableController(ITableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        // API để lấy bàn theo ID
        [HttpGet("/{id:int}")]
        public async Task<IActionResult> GetTableById(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table); // Trả về đối tượng đã được hoàn thành, không phải là `Task`
        }

    }
}
