using Microsoft.EntityFrameworkCore;
using WEBAPP.Data;
using WEBAPP.Models;
using WEBAPP.Repository.IRepository;

namespace WEBAPP.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var product = _db.Products.FirstOrDefault(u => u.product_id == obj.product_id);
            if (product != null)
            {
                product.product_id = obj.product_id;
                product.product_name = obj.product_name;
                product.product_price = obj.product_price;
                product.product_description = obj.product_description;
                product.CategoryId = obj.CategoryId;
                if (obj.product_image_link != null)
                {
                    product.product_image_link = obj.product_image_link;
                }
            }
        }
    }
}
