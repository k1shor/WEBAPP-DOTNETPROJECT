using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using WEBAPP.Models;
using WEBAPP.Repository;
using WEBAPP.Repository.IRepository;

namespace WEBAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork uOW;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork uow)
        {
            _logger = logger;
            uOW = uow;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productlist = uOW.Product.GetAllData(includeProperties:"Category");
            return View(productlist);
        }

        public IActionResult MailSent()
        {
            var client = new SmtpClient("smtp.mailtrap.io", 2525)
            {
                Credentials = new NetworkCredential("98d01484d2ca26", "e75a30a02b5212"),
                EnableSsl = true
            };
            client.Send("from@example.com", "to@example.com", "Hello world", "testbody");
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Authorize]
        public IActionResult ProductDetails(int? productId)
        {
            ShoppingCart cartObj = new()
            {
                count = 1,
                Product = uOW.Product.GetFirstOrDefault(u => u.product_id == productId),
                ProductId = (int)productId,
            };
            return View(cartObj);
        }
        [HttpPost]
        public IActionResult ProductDetails(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            ShoppingCart cart = uOW.ShoppingCart.GetFirstOrDefault(u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);

            if(cart == null)
            {
                uOW.ShoppingCart.Add(shoppingCart);
            }
            else
            {
                uOW.ShoppingCart.IncrementCount(cart, shoppingCart.count);
            }

            uOW.Save();
            return RedirectToAction("Index");
        }
        
    }
}