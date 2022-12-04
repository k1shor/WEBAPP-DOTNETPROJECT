using WEBAPP.Models;

namespace WEBAPP.Repository.IRepository
{
    public interface IOrderRepository : IRepository <Order>
    {
        void Update(Order obj);
    }
}
