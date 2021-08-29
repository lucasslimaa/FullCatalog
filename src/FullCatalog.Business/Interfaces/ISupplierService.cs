using System.Threading.Tasks;
using System;

namespace FullCatalog.Business.Services
{
    public interface ISupplierService : IDisposable
    {
        Task Add(Supplier supplier);
        Task Update(Supplier supplier);
        Task Delete(Guid id);
        Task UpdateAddress(Address address);

    }
}