using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DeleteTime { get; set; }

        protected BaseEntity(int id, bool isDeleted, DateTime createdDate)
        {
            Id = id;
            IsDeleted = isDeleted;
            CreatedDate = createdDate;
        }

        protected BaseEntity()
        {
            this.Id = 0;
            this.IsDeleted = false;
            this.CreatedDate = DateTime.UtcNow;
        }

        public void DeleteEntity()
        {
            IsDeleted = true;
            DeleteTime = DateTime.UtcNow;
        }
    }
}
