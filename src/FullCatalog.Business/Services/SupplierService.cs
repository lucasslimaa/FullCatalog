using FullCatalog.Business.Interfaces;
using FullCatalog.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FullCatalog.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(ISupplierRepository supplierRepository,
                               IAddressRepository addressRepository,
                               INotifier notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }
        public async Task Add(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)
                || !ExecuteValidation(new AddressValidation(), supplier.Address)) return;

            if (_supplierRepository.Search(f => f.DocumentNumber == supplier.DocumentNumber).Result.Any())
            {
                Notify("There is already a supplier registered with this document number!");
                return;
            }

            await _supplierRepository.Add(supplier);
        }

        public async Task Update(Supplier supplier)
        {
            if (!ExecuteValidation(new SupplierValidation(), supplier)) return;

            if (_supplierRepository.Search(f => f.DocumentNumber == supplier.DocumentNumber && f.Id != supplier.Id).Result.Any())
            {
                Notify("There is already a supplier registered with this document number!");
                return;
            }

            await _supplierRepository.Update(supplier);


        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address)) return;

            await _addressRepository.Update(address);

        }

        public async Task Delete(Guid id)
        {
            if (_supplierRepository.GetSupplierProductsAddress(id).Result.Products.Any())
            {
                Notify("There is products attached to this supplier!");
                return;
            }

            await _supplierRepository.Remove(id);
        }

        public void Dispose()
        {
            _supplierRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}
