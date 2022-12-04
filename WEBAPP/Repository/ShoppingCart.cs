using WEBAPP.Data;
using WEBAPP.Models;
using WEBAPP.Repository.IRepository;

namespace WEBAPP.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        ApplicationDbContext _db;
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementCount(ShoppingCart cart)
        {
            cart.count--;
            return cart.count;
        }

        public int IncrementCount(ShoppingCart cart, int? count = 0)
        {
            if(count == null)
            {
                cart.count++;
            }
            else
            {
            cart.count = (int)(cart.count + count);
            }
            return cart.count;
        }
    }
}
