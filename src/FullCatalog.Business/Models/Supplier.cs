using FullCatalog.Business;
using System.Collections.Generic;

namespace FullCatalog.Business
{
    public class Supplier : Entity
    {
        public string Name { get; set; }
        public string DocumentNumber { get; set; }
        public SupplierType SupplierType { get; set; }
        public Address Address { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
