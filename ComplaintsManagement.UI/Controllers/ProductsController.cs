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

        public ActionResult Edit(int id)
        {
            return View();
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

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _productsRepository.DeleteAsync(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}