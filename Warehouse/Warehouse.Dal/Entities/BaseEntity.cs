using System.ComponentModel.DataAnnotations;

namespace Warehouse.Dal.Entities
{
    public class BaseEntity
    {
        [Key] public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
