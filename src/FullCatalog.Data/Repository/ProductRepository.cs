using FullCatalog.Business;
using FullCatalog.Business.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullCatalog.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(CatalogDbContext context) : base(context) { }
        public async Task<Product> GetSupplierProduct(Guid id)
        {
            return await Db.Products.AsNoTracking().Include(s => s.Supplier)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        
        public async Task<IEnumerable<Product>> GetProductsSuppliers()
        {
            return await Db.Products.AsNoTracking().Include(s => s.Supplier)
                .OrderBy(p => p.Name).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetProductsBySupplier(Guid supplierId)
        {
            var a = await Search(p => p.SupplierId == supplierId);
            return await Search(p => p.SupplierId == supplierId);
        }
    }
}
