using WEBAPP.Models;

namespace WEBAPP.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product obj);
    }
}
