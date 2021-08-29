using AutoMapper;
using FullCatalog.App.Extensions;
using FullCatalog.App.ViewModels;
using FullCatalog.Business;
using FullCatalog.Business.Interfaces;
using FullCatalog.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FullCatalog.App.Controllers
{
    [Route("supplier")]
    [Authorize]
    public class SuppliersController : BaseController
    {

        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierService _supplierService;
        private readonly IMapper _mapper;

        public SuppliersController(ISupplierRepository supplierRepository, IAddressRepository addressRepository,
                                    IMapper mapper, ISupplierService supplierService,
                                    INotifier notifier) : base(notifier)
        {
            _supplierRepository = supplierRepository;
            _supplierService = supplierService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("list")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll()));
        }

        [AllowAnonymous]
        [Route("info/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);

            if (supplierViewModel == null)
            {
                return NotFound();
            }

            return View(supplierViewModel);
        }

        [ClaimsAuthorize("Supplier", "Add")]
        [Route("new")]
        public IActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Supplier", "Add")]
        [Route("new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SupplierViewModel supplierViewModel)
        {
            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);
            await _supplierService.Add(supplier);

            if (!OperationIsValid()) return View(supplierViewModel);


            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Supplier", "Edit")]
        [Route("edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var supplierViewModel = await GetSupplierProductsAddress(id);

            if (supplierViewModel == null) return NotFound();

            return View(supplierViewModel);
        }

        [ClaimsAuthorize("Supplier", "Edit")]
        [Route("edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, SupplierViewModel supplierViewModel)
        {
            if (id != supplierViewModel.Id) return NotFound();

            if (!ModelState.IsValid) return View(supplierViewModel);

            var supplier = _mapper.Map<Supplier>(supplierViewModel);

            await _supplierService.Update(supplier);

            if (!OperationIsValid()) return View(supplierViewModel);


            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Supplier", "Delete")]
        [Route("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var supplierViewModel = await GetSupplierAddress(id);

            if (supplierViewModel == null) return NotFound();

            return View(supplierViewModel);
        }

        [ClaimsAuthorize("Supplier", "Delete")]
        [Route("delete/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var supplier = await GetSupplierAddress(id);

            if (supplier == null) return NotFound();

            await _supplierService.Delete(id);

            if (!OperationIsValid()) return View(supplier);

            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [Route("get-supplier-address/{id:guid}")]
        public async Task<IActionResult> GetAddress(Guid id)
        {
            var supplier = await GetSupplierAddress(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return PartialView("_AddressDetails", supplier);
        }

        [Route("update-supplier-address/{id:guid}")]
        public async Task<IActionResult> UpdateAddress(Guid id)
        {
            var supplier = await GetSupplierAddress(id);

            if (supplier == null)
            {
                return NotFound();
            }

            return PartialView("_UpdateAddress", new SupplierViewModel { Address = supplier.Address });
        }

        [HttpPost]
        [Route("update-supplier-address/{id:guid}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(SupplierViewModel supplierViewModel)
        {
            ModelState.Remove("DocumentNumber");
            ModelState.Remove("Name");

            if (!ModelState.IsValid) return PartialView("_UpdateAddress", supplierViewModel);

            await _supplierService.UpdateAddress(_mapper.Map<Address>(supplierViewModel.Address));

            if (!OperationIsValid()) return PartialView("_UpdateAddress", supplierViewModel);

            var url = Url.Action("GetAddress", "Suppliers", new { Id = supplierViewModel.Address.SupplierId });

            return Json(new { success = true, url });
        }

        private async Task<SupplierViewModel> GetSupplierAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierAddress(id));
        }

        private async Task<SupplierViewModel> GetSupplierProductsAddress(Guid id)
        {
            return _mapper.Map<SupplierViewModel>(await _supplierRepository.GetSupplierProductsAddress(id));
        }

    }
}
