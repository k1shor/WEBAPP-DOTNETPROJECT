using WEBAPP.Models;

namespace WEBAPP.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository <ShoppingCart>
    {
        int IncrementCount(ShoppingCart cart, int? count = null);
        int DecrementCount(ShoppingCart cart);
    }
}
