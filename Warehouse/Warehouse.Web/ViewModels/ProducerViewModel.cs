using System.ComponentModel.DataAnnotations;

namespace Warehouse.Web.ViewModels
{
    public class ProducerViewModel
    {        
        public int Id { get; set; }

        [Display(Name = "Producer")]
        [Required(ErrorMessage = "Please enter the Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter the Description")]
        public string Description { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Please enter the Phone")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter the Address")]
        public string Address { get; set; }

        public bool IsDeleted { get; set; }
    }
}
