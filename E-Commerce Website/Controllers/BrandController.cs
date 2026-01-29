using E_Commerce_Website.Core.DTO;
using E_Commerce_Website.Core.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Website.Controllers
{
    [Authorize]
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
        public async Task<BrandPaginationResponse> GetAllBrands([FromBody]BrandPaginationRequest request)
        {
            return await _brandService.GetAllBrands(request);
        }

        [HttpPost("DeleteBrand")]
        public async Task<BrandActionResponse> DeleteBrand([FromBody]DeleteBrandRequest request)
        {
            return await _brandService.DeleteBrand(request);
        }

        [HttpGet("Dropdown")]
        public async Task<List<BrandDropDown>> GetBrandDropDown()
        {
            return await _brandService.GetBrandDropDown();
        }
    }
}
