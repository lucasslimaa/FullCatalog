using FullCatalog.Business.Interfaces;
using FullCatalog.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace FullCatalog.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
                              INotifier notifier) : base(notifier)
        {
            _productRepository = productRepository;
        }

        public async Task Add(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Add(product);
        }
       
        public async Task Update(Product product)
        {
            if (!ExecuteValidation(new ProductValidation(), product)) return;

            await _productRepository.Update(product);
        }

        public async Task Delete(Guid id)
        {
            await _productRepository.Remove(id);
        }


        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
