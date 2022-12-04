using WEBAPP.Data;
using WEBAPP.Models;
using WEBAPP.Repository.IRepository;

namespace WEBAPP.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        ApplicationDbContext _db;
        public OrderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Order obj)
        {
            _db.Orders.Update(obj);
        }
    }
}
