using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WEBAPP.Models;
using WEBAPP.Repository;
using WEBAPP.Repository.IRepository;

namespace WEBAPP.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        IUnitOfWork unitofwork;

        public ShoppingCartController(IUnitOfWork uow)
        {
            unitofwork = uow;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<ShoppingCart> shoppingcart = unitofwork.ShoppingCart.GetAllData(includeProperties:"Product").Where(u=>u.ApplicationUserId==claim.Value);
            return View(shoppingcart);
            
        }

        public IActionResult CartItemUpdate(int id)
        {
            ShoppingCart cart = unitofwork.ShoppingCart.GetFirstOrDefault(u => u.ID == id, includeProperties:"Product");
            return View(cart); 
        }


        public IActionResult IncreaseCount(int cartId, int? count)
        {
            ShoppingCart cart = unitofwork.ShoppingCart.GetFirstOrDefault(u => u.ID == cartId, includeProperties: "Product");
            if (count == null)
            {
            unitofwork.ShoppingCart.IncrementCount(cart, cart.count);
            }
            else
            {
                unitofwork.ShoppingCart.IncrementCount(cart);
            }
            unitofwork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult IncrementCount(int cartId)
        {
            return IncreaseCount(cartId, 1);
        }


        public IActionResult DecrementCount(int cartId)
        {
            ShoppingCart cart = unitofwork.ShoppingCart.GetFirstOrDefault(u => u.ID == cartId, includeProperties: "Product");

            unitofwork.ShoppingCart.DecrementCount(cart);
            unitofwork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int cartId)
        {
            ShoppingCart cart = unitofwork.ShoppingCart.GetFirstOrDefault(u => u.ID == cartId);
            unitofwork.ShoppingCart.Remove(cart);
            unitofwork.Save();
            return RedirectToAction("Index");
        }

    }
}
