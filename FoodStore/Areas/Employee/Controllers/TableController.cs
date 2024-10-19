using FoodStore.Models;
using FoodStore.Repositories;
using IronBarCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.Language.CodeGeneration;
using System.Security.Claims;

namespace FoodStore.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles = SD.Role_Employee)]
    public class TableController : Controller
    {
        private readonly ITableRepository _banRepository;
        private readonly ApplicationDbContext _context;
        public TableController(ITableRepository banRepository, ApplicationDbContext context)
        {
            _banRepository = banRepository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var bans = await _banRepository.GetAllAsync();
            return View(bans);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Thực hiện thêm bàn mới vào cơ sở dữ liệu
            var table = await _banRepository.AddAsync();
                GenerateQR(table.Id);
                return RedirectToAction("Index");
        } 
        public async Task<IActionResult> Display(int id)
        {
            var ban = await _banRepository.GetByIdAsync(id);
            if (ban == null)
            {
                return NotFound();
            }
            return View(ban);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {

            var ban = await _banRepository.GetByIdAsync(id);
            if (ban == null)
            {
                return NotFound();
            }
            return View(ban);
        }
        // Xử lý cập nhật bàn
        [HttpPost]
        public async Task<IActionResult> Update(int id, Table ban)
        {
            if (id != ban.Id)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                var banupdate = await _banRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync
                // Cập nhật các thông tin khác của sản phẩm
                banupdate.Id = ban.Id;
                banupdate.Status = ban.Status;
                await _banRepository.UpdateAsync(banupdate);
                return RedirectToAction(nameof(Index));

            }
            return View(ban);
        }
     
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _banRepository.DeleteAsync(id);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private void GenerateQR(int idTable)
        {
            string url = "http://nguyenthuytruc-001-site1.dtempurl.com/customer/order/" + idTable;
            string path = "wwwroot/images/QRTable/";
            string fileName = idTable.ToString() + ".png";
            // Tạo mã QR từ URL được cung cấp
            GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(url, 250);
            barcode.SetMargins(10);

            barcode.SaveAsImage(path + fileName);

            Path.Combine(path, fileName);

        }
    }
}

