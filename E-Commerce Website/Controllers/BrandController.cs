using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.IService;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Website.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost("AddOrUpdateBrand")]
        public async Task<BrandResponseDto> AddOrUpdateBrand([FromBody] BrandDto dto)
        {
           return  await _brandService.AddOrUpdateBrand(dto);
        }

        [HttpGet("GetAllBrands")]
        public IQueryable<BrandListResponseDto> GetAllBrands()
        {
            return _brandService.GetAllBrands();
        }

        [HttpPost("GetBrandById")]
        public async Task<BrandListResponseDto?> GetBrandById(int id)
        {
            return await _brandService.GetBrandById(id);
        }

        [HttpPost("DeleteBrand")]
        public async Task<DeleteBrandResponseDto> DeleteBrand(int id)
        {
            return await _brandService.DeleteBrand(id);
        }
    }
}
