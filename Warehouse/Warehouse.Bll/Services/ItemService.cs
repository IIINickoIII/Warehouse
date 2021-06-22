using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using Warehouse.Bll.Dtos;
using Warehouse.Bll.Interfaces;
using Warehouse.Dal.Entities;
using Warehouse.Dal.Interfaces;

namespace Warehouse.Bll.Services
{
    public class ItemService : IItemService
    {
        public ItemService(IMapper mapper, IUnitOfWork uow)
        {
            _mapper = mapper;
            _uow = uow;
            _includes = items => items
                .Include(item => item.Producer)
                .Include(item => item.Category);
        }

        private readonly Func<IQueryable<Item>, IIncludableQueryable<Item, object>> _includes;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _uow;

        public void AddItem(ItemDto itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);
            _uow.ItemRepository.Add(item);
            _uow.Save();
        }

        public void EditItem(ItemDto itemDto)
        {
            var item = _mapper.Map<Item>(itemDto);
            _uow.ItemRepository.Update(item);
            _uow.Save();
        }

        public IEnumerable<ItemDto> GetAllItems()
        {
            var itemsInDb = _uow.ItemRepository.GetAll(_includes);
            var itemDtos = _mapper.Map<IEnumerable<ItemDto>>(itemsInDb);
            return itemDtos;
        }

        public ItemDto GetItem(int itemId)
        {
            var ItemInDb = _uow.ItemRepository.Single(i => i.Id == itemId && i.IsDeleted == false, _includes);
            var itemDto = _mapper.Map<ItemDto>(ItemInDb);
            return itemDto;
        }

        public void SoftDelete(int itemId)
        {
            var itemInDb = _uow.ItemRepository.Single(i => i.Id == itemId);
            _uow.ItemRepository.SoftDelete(itemInDb);
            _uow.Save();
        }
    }
}
