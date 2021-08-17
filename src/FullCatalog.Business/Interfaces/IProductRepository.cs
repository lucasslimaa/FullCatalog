using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCatalog.Business.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetSupplierProduct(Guid id);
        Task<IEnumerable<Product>> GetProductsSuppliers();
        Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId);      
    }
}
