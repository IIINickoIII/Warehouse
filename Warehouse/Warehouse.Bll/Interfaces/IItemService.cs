using System.Collections.Generic;
using Warehouse.Bll.Dtos;

namespace Warehouse.Bll.Interfaces
{
    public interface IItemService
    {
        void AddItem(ItemDto itemDto);
        void EditItem(ItemDto itemDto);
        ItemDto GetItem(int itemId);
        IEnumerable<ItemDto> GetAllItems();
        void SoftDelete(int itemId);
    }
}
