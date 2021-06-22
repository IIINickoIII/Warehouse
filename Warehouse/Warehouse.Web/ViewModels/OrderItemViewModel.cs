using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Warehouse.Bll.Dtos;

namespace Warehouse.Web.ViewModels
{
    public class OrderItemViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Item")]
        [Required(ErrorMessage = "Please enter the Item")]
        public int ItemId { get; set; }
        public ItemDto Item { get; set; }

        [Display(Name = "Order")]
        [Required(ErrorMessage = "Please enter the Order")]
        public int OrderId { get; set; }
        public OrderDto Order { get; set; }

        [Display(Name = "Price")]
        [Required(ErrorMessage = "Please enter the Price")]
        public decimal Price { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please enter the Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Discount")]
        [Required(ErrorMessage = "Please enter the Discount")]
        public decimal Discount { get; set; }

        [Display(Name = "Sum Without Discount")]
        [Required(ErrorMessage = "Please enter the Sum Without Discount")]
        public decimal SumWithoutDiscount { get; set; }

        [Display(Name = "Sum With Discount")]
        [Required(ErrorMessage = "Please enter the Sum With Discount")]
        public decimal SumWithDiscount { get; set; }

        public decimal IsDeleted { get; set; }

        public IEnumerable<ItemDto> AllItemList { get; set; }
        public IEnumerable<SelectListItem> ItemSelectList
        {
            get
            {
                if (AllItemList == null)
                {
                    return new List<SelectListItem>();
                }

                var selectList = AllItemList
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                    .ToImmutableList()
                    .Add(new SelectListItem("Select Item", "0", true, true));
                return Item == null ? selectList : SetSelectedItems(selectList, new List<int>() { ItemId });
            }
        }
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
