using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.IService;
using E_Commerce_Website.Enum;
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
        public async Task<BrandActionResponse> AddOrUpdateBrand([FromBody] BrandRequest request)
        {
           return  await _brandService.AddOrUpdateBrand(request);
        }

        [HttpPost("GetAllBrands")]
        public async Task<BrandPaginationResponse> GetAllBrands(PaginationRequest request)
        {
            return await _brandService.GetAllBrands(request);
        }

        [HttpPost("DeleteBrand")]
        public async Task<BrandActionResponse> DeleteBrand(int id)
        {
            return await _brandService.DeleteBrand(id);
        }

        [HttpGet("Dropdown")]
        public async Task<List<BrandDropDown>> GetBrandDropDown()
        {
            return await _brandService.GetBrandDropDown();
        }
    }
}
