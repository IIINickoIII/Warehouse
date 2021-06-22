using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Warehouse.Bll.Dtos;

namespace Warehouse.Web.ViewModels
{
    public class PaymentViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Date")]
        [Required(ErrorMessage = "Please enter the Date")]
        public DateTime Date { get; set; }
        [Display(Name = "Total Sum")]
        [Required(ErrorMessage = "Please enter the Total Sum")]
        public decimal TotalSum { get; set; }
        [Display(Name = "Client")]
        [Required(ErrorMessage = "Please enter the Client")]
        public int ClientId { get; set; }
        public ClientDto Client { get; set; }
        [Display(Name = "Income")]
        public bool IsInCome { get; set; }
        public bool IsDeleted { get; set; }

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
