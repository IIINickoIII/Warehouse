using System.ComponentModel.DataAnnotations;

namespace Warehouse.Web.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please enter the Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter the Description")]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
}
