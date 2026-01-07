using E_Commerce_Website.Core.Enitities;

namespace E_Commerce_Website.Core.IRepository
{
    public interface IBrandRepo
    {
        Task<int> AddBrand(Brand brand);
        Task<int> UpdateBrand(Brand brand);
        IQueryable<Brand> GetAllBrands();
        Task<List<Brand>> GetActiveBrandsAsync();
        Task<Brand?> GetBrandById(int id);
    }
}

