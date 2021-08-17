using System;

namespace FullCatalog.Business
{
    public class Address : Entity
    {
        public Guid SupplierId { get; set; }
        public string Street { get; set; } 
        public string Neighborhood { get; set; }
        public string Number { get; set; }
        public string ApartmentNumber { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Supplier Supplier { get; set; }
    }
}
