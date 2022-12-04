using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WEBAPP.Models
{
    public class Product
    {
        [Key]
        public int product_id{ get; set; }
        [Required(ErrorMessage ="Product name is required")]
        [MaxLength(20, ErrorMessage ="Product name must not be more than 20 characters")]
        [DisplayName("Product Name")]
        public string product_name { get; set; }
        [Range(0,1000000, ErrorMessage ="Price out of range")]
        [Required(ErrorMessage ="Product price is required")]
        [DisplayName("Price")]
        public double product_price { get; set; }
        [Required]
        [MinLength(30, ErrorMessage = "Description must be at least 30 characters")]
        [DisplayName("Description")]
        public string product_description { get; set; }
        [ValidateNever]
        [DisplayName("Image")]
        public string product_image_link { get; set; }
        [DisplayName("Rating")]
        public int rating { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }
        public DateTime createdAt { get; set; }
    }
}
