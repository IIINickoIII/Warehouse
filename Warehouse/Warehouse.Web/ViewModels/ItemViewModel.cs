using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Warehouse.Bll.Dtos;

namespace Warehouse.Web.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Please enter the Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Please enter the Description")]
        public string Description { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please enter the Price")]
        public decimal Price { get; set; }

        [Display(Name = "Discount")]
        public decimal Discount { get; set; }

        [Display(Name = "Unit In Stock")]
        public int UnitInStock { get; set; }

        [Display(Name = "Producer")]
        [Required(ErrorMessage = "Please enter the Producer")]
        public int ProducerId { get; set; }
        public ProducerDto Producer { get; set; }

        public IEnumerable<ProducerDto> AllProducerList { get; set; }

        public IEnumerable<SelectListItem> ProducerSelectList
        {
            get
            {
                if (AllProducerList == null)
                {
                    return new List<SelectListItem>();
                }

                var selectList = AllProducerList
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                    .ToImmutableList()
                    .Add(new SelectListItem("Select Producer", "0", true, true));
                return Producer == null ? selectList : SetSelectedItems(selectList, new List<int>() { ProducerId });
            }
        }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please enter the Category")]
        public int CategoryId { get; set; }
        public CategoryDto Category { get; set; }

        public IEnumerable<CategoryDto> AllCategoryList { get; set; }

        public IEnumerable<SelectListItem> CategorySelectList
        {
            get
            {
                if (AllCategoryList == null)
                {
                    return new List<SelectListItem>();
                }

                var selectList = AllCategoryList
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                    .ToImmutableList()
                    .Add(new SelectListItem("Select Category", "0", true, true));
                return Category == null ? selectList : SetSelectedItems(selectList, new List<int>() { CategoryId });
            }
        }
        public bool IsDeleted { get; set; }

        public static IEnumerable<SelectListItem> SetSelectedItems(IEnumerable<SelectListItem> list, IEnumerable<int> itemIds)
        {
            foreach (var item in list)
            {
                item.Selected = itemIds.Contains(Convert.ToInt32(item.Value));
            }

            return list;
        }
    }
}
