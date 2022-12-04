using WEBAPP.Models;

namespace WEBAPP.Repository.IRepository
{
    public interface IApplicationUserRepository : IRepository <ApplicationUser>
    {
        void Update(ApplicationUser obj);
    }
}
