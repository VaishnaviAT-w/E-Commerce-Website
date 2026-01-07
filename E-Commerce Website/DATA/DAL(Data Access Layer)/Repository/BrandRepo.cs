using E_Commerce_Website.Core.Enitities;
using E_Commerce_Website.Core.IRepository;
using E_Commerce_Website.Data;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce_Website.DATA.DAL_Data_Access_Layer_.Repository
{
    public class BrandRepo : IBrandRepo
    {
        private readonly ApplicationDBContext _context;
        public BrandRepo(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<int> AddBrand(Brand brand)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return brand.BrandId;
        }

        public async Task<int> UpdateBrand(Brand brand)
        {
            _context.Brands.Update(brand);
             await _context.SaveChangesAsync();
            return brand.BrandId;
        } 

        public IQueryable<Brand> GetAllBrands()
        {
            return _context.Brands.Where(x => x.IsActive);
        }

        public async Task<List<Brand>> GetActiveBrandsAsync()
        {
            return await _context.Brands
                .Where(x => x.IsActive)       
                .OrderBy(x => x.BrandName)    
                .ToListAsync();              
        }

        public async Task<Brand?> GetBrandById(int id)
        {
            return await _context.Brands
                           .FirstOrDefaultAsync(x => x.BrandId == id && x.IsActive == true);
        }
    }
}
