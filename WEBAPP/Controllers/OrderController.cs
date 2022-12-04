using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stripe.Checkout;
using System.Security.Claims;
using WEBAPP.Models;
using WEBAPP.Repository;
using WEBAPP.Repository.IRepository;

namespace WEBAPP.Controllers
{
    public class OrderController : Controller
    {
        IUnitOfWork unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public IActionResult Index(int cartId)
        {
            ShoppingCart cart = unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.ID == cartId, includeProperties: "Product");
            Order order = new()
            {
                ApplicationUserId = cart.ApplicationUserId,
                ApplicationUser = cart.ApplicationUser,
                ProductId = cart.ProductId,
                Product = cart.Product,
                Shipping_Charge = 100,
                OrderTotal = cart.Product.product_price * cart.count,
                count = cart.count,
                ShoppingCartId = cart.ID

            };
            return View(order);
        }

        [HttpPost]
        public IActionResult Index(Order order)
        {
            ShoppingCart cart = unitOfWork.ShoppingCart.GetFirstOrDefault(u => u.ID == order.ShoppingCartId);

            unitOfWork.Order.Add(order);

            /*stripe settings      */
            var domain = "https://localhost:44391";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                      UnitAmount = (long?)order.OrderTotal * 100,
                      Currency = "npr",
                      ProductData = new SessionLineItemPriceDataProductDataOptions
                      {
                        Name = order.Product.product_name,
                      },
                    },
                    Quantity = order.count,
                  },
                },
                Mode = "payment",
                SuccessUrl = domain + $"/PaymentSuccess",
                CancelUrl = domain + $"/ShoppingCart",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);





            /*unitOfWork.ShoppingCart.Remove(cart);
            unitOfWork.Save();

            return RedirectToAction("MakePayment");*/
        }

        public IActionResult PaymentSuccess()
        {

            return View();
        }
    }
}

