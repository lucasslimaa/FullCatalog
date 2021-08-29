using System;
using System.Threading.Tasks;

namespace FullCatalog.Business.Services
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Guid id);

    }
}