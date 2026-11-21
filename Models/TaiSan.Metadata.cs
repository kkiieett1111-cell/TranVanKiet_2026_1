using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace TranVanKiet_2026_1.Models
{
    [ModelMetadataType(typeof(TaiSanMetadata))]
    public partial class TaiSan { }

    public class TaiSanMetadata
    {
        [Required(ErrorMessage = "Vui lòng chọn loại tài sản")]
        [Display(Name = "Loại tài sản")]
        public int MaLoai { get; set; }

        [Required(ErrorMessage = "Tên tài sản không được để trống")]
        [Display(Name = "Tên tài sản")]
        public string TenTs { get; set; } = string.Empty;

        [Required(ErrorMessage = "Nguyên giá không được để trống")]
        [Range(1, 2000, ErrorMessage = "Nguyên giá phải là số từ 1 đến 2000")]
        [Display(Name = "Nguyên giá (triệu)")]
        public decimal? NguyenGia { get; set; }

        [Required(ErrorMessage = "Tên file ảnh không được để trống")]
        [RegularExpression(@".*\.(jpg|jpeg|png|webp)$", ErrorMessage = "Ảnh phải có đuôi .jpg, .jpeg, .png hoặc .webp")]
        [Display(Name = "Tên file ảnh")]
        public string? Anh { get; set; }
    }
}