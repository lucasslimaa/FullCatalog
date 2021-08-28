using AutoMapper;
using FullCatalog.App.ViewModels;
using FullCatalog.Business;
using FullCatalog.Business.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace FullCatalog.App.Controllers
{
    [Route("product")]
    public class ProductsController : BaseController
    {
        private IWebHostEnvironment _hostEnvironment;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ISupplierRepository _supplierRepository;

        public ProductsController(IProductRepository productRepository,
                                  ISupplierRepository supplierRepository,
                                  IMapper mapper, IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _supplierRepository = supplierRepository;
            _hostEnvironment = environment;
        }


        [Route("list")]
        public async Task<IActionResult> Index()
        {
            return View (_mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsSuppliers()));
        }

        [Route("details/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        [Route("new")]
        public async Task<IActionResult> Create()
        {
            var ProductViewModel = await PopulateSupplier(new ProductViewModel());
            return View(ProductViewModel);
        }

        [Route("new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await PopulateSupplier(productViewModel);

            if (!ModelState.IsValid) return View(productViewModel);

            var imgPrefix = Guid.NewGuid() + "_";
            if (! await UploadFile(productViewModel.ImageUpload, imgPrefix))
            {
                return View(productViewModel);
            }

            productViewModel.Image = imgPrefix + productViewModel.ImageUpload.FileName;

            await _productRepository.Add(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");
        }

        [Route("edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null) return NotFound();

            return View(productViewModel);
        }

        [HttpPost]
        [Route("edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, ProductViewModel productViewModel)
        {
            if (id != productViewModel.Id) return NotFound();

            var productRefresh = await GetProduct(id);
            productViewModel.Supplier = productRefresh.Supplier;
            productViewModel.Image = productRefresh.Image;

            if (!ModelState.IsValid) return View(productViewModel);

            if (productViewModel.ImageUpload != null)
            {
                var imgPrefix = Guid.NewGuid() + "_";
                if (!await UploadFile(productViewModel.ImageUpload, imgPrefix))
                {
                    return View(productViewModel);
                }

                productRefresh.Image = imgPrefix + productViewModel.ImageUpload.FileName;
            }

            productRefresh.Name = productViewModel.Name;
            productRefresh.Description = productViewModel.Description;
            productRefresh.Value = productViewModel.Value;
            productRefresh.IsActive = productViewModel.IsActive;

            var product = _mapper.Map<Product>(productRefresh);
            await _productRepository.Update(product);

            return RedirectToAction("Index");
        }

        [Route("delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null) return NotFound();

            return View(product);
        }

        [Route("delete/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await GetProduct(id);

            if (product == null) return NotFound();

            await _productRepository.Remove(id);

            return RedirectToAction("Index");
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetSupplierProduct(id));
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll());
            return product;
        }

        private async Task<ProductViewModel> PopulateSupplier(ProductViewModel product)
        {
            product.Suppliers = _mapper.Map<IEnumerable<SupplierViewModel>>(await _supplierRepository.GetAll());
            return product;
        }

        private async Task<bool> UploadFile(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
