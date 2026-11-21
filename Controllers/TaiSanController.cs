using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TranVanKiet_2026_1.Models;

namespace TranVanKiet_2026_1.Controllers
{
    public class TaiSanController : Controller
    {
        private readonly QLTaiSanContext _context;

        public TaiSanController(QLTaiSanContext context)
        {
            _context = context;
        }

      
        public async Task<IActionResult> Index()
        {
            var danhSach = await _context.TaiSans
                .Include(t => t.MaLoaiNavigation)
                .Where(t => t.NguyenGia != null && t.NguyenGia >= 20)
                .ToListAsync();

            ViewBag.SectionTitle = "Trần Văn Kiệt";
            return View(danhSach);
        }

        [HttpGet]
        public async Task<IActionResult> FilterByLoai(int maLoai)
        {
            var query = _context.TaiSans
                .Include(t => t.MaLoaiNavigation)
                .Where(t => t.NguyenGia != null && t.NguyenGia >= 20);

            if (maLoai > 0)
            {
                query = query.Where(t => t.MaLoai == maLoai);
                var loai = await _context.LoaiTaiSans.FindAsync(maLoai);
                ViewBag.SectionTitle = loai?.TenLoai ?? "Tài sản";
            }
            else
            {
                ViewBag.SectionTitle = "Trần Văn Kiệt";
            }

            var ketQua = await query.ToListAsync();
            return PartialView("_DanhSachTaiSan", ketQua);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.MaLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai");
            return View();
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TaiSan taiSan)
        {
            if (ModelState.IsValid)
            {
                _context.TaiSans.Add(taiSan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MaLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai", taiSan.MaLoai);
            return View(taiSan);
        }
    }
}