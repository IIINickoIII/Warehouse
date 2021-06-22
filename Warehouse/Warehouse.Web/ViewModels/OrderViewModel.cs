using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Warehouse.Bll.Dtos;

namespace Warehouse.Web.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Client")]
        [Required(ErrorMessage = "Please enter the Client")]
        public int ClientId { get; set; }
        public ClientDto Client { get; set; }
        public bool IsPayed { get; set; }
        [Display(Name = "Delivered")]
        public bool IsDelivered { get; set; }
        public bool IsClosedForEdit { get; set; }
        [Display(Name = "Total Sum")]
        public decimal TotalSum { get; set; }
        public IEnumerable<OrderItemDto> OrderItems { get; set; }
        public bool IsDeleted { get; set; }
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Income")]
        public bool IsIncome { get; set; }

        public IEnumerable<ClientDto> AllClientList { get; set; }

        public IEnumerable<SelectListItem> ClientSelectList
        {
            get
            {
                if (AllClientList == null)
                {
                    return new List<SelectListItem>();
                }

                var selectList = AllClientList
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString()))
                    .ToImmutableList()
                    .Add(new SelectListItem("Select Client", "0", true, true));
                return Client == null ? selectList : SetSelectedItems(selectList, new List<int>() { ClientId });
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
