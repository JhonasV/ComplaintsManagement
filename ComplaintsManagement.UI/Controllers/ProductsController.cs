using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComplaintsManagement.UI.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IProductsRepository _productsRepository;

        public ProductsController(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _productsRepository.GetAllAsync();
            return View(model);
        }


        public async Task<ActionResult> Details(int id)
        {
            var model = await _productsRepository.GetAsync(id);

            return View(model);
        }



        public ActionResult Create()
        {
            var model = new TaskResult<ProductsDto>();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductsDto Data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var newModel = await _productsRepository.SaveAsync(Data);

            return View(newModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index));
            }

            var model = await _productsRepository.GetAsync(id);
            

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ProductsDto Data)
        {

            if (!ModelState.IsValid)
            {
                return View(new TaskResult<ProductsDto> { Data = Data });
            }

            var model = await _productsRepository.UpdateAsync(Data);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var deleteResult = await _productsRepository.DeleteAsync(Id);
            ViewData["Message"] = deleteResult.Message;
            ViewData["Success"] = deleteResult.Success;
            return RedirectToAction(nameof(this.Index));
        }
    }
}