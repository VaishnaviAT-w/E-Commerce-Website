using E_Commerce_Website.Core.Contract.IService;

namespace E_Commerce_Website.BI.Service
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int UserId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?
                    .User?
                    .Claims
                    .FirstOrDefault(x => x.Type == "UserId");

                return claim != null ? Convert.ToInt32(claim.Value) : 0;
            }
        }
    }
}
