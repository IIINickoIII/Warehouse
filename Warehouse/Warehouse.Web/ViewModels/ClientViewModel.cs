using System.ComponentModel.DataAnnotations;

namespace Warehouse.Web.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter the Name")]
        public string Nickname { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please enter the First Name")]
        public string Name { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Please enter the Last Name")]
        public string Surname { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Please enter the Phone")]
        public string Phone { get; set; }

        [Display(Name = "Address")]
        [Required(ErrorMessage = "Please enter the Address")]
        public string Address { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Customer")]
        public bool IsCustomer { get; set; }

        [Display(Name = "Provider")]
        public bool IsProvider { get; set; }
        public bool IsDeleted { get; set; }
    }
}
