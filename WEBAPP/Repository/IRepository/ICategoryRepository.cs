using WEBAPP.Models;

namespace WEBAPP.Repository.IRepository
{
    public interface ICategoryRepository : IRepository <Category>
    {
        void Update(Category obj);
    }
}
