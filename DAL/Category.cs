using System.ComponentModel.DataAnnotations;

namespace DAL
{
    public class Category
    {

        [Display(Name="Category")]
        public int CategoryId { get; set; }
        public string Name { get; set; }



        public virtual ICollection<Product> Products { get; set; }
    }
}