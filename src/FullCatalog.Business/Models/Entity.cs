using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCatalog.Business
{
    public abstract class Entity
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        protected Entity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
