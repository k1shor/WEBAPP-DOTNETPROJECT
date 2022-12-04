using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WEBAPP.Models
{
    public class Category
    {
        [Key]
        public int category_id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        public string category_name { get; set; }
        [DisplayName("Created")]
        public DateTime createdAt { get; set; } = DateTime.Now;
    }
}
