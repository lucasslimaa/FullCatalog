using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FullCatalog.Business
{
    public class Product : Entity
    {
        public Guid SupplierId { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public string Image { get; set; }
        public decimal Value { get; set; }
        public DateTime CreateDateTime { get; set; }
        public bool IsActive { get; set; }
        public Supplier Supplier { get; set; }
    }
}
