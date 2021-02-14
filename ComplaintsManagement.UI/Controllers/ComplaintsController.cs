using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComplaintsManagement.UI.Controllers
{
    public class ComplaintsController : Controller
    {
        private readonly IComplaintsRepository _complaintsRepository;
        private readonly IComplaintsOptionsRepository _complaintsOptionsRepository;
        private readonly IProductsRepository _productsRepository;

        public ComplaintsController(
            IComplaintsRepository complaintsRepository,
            IComplaintsOptionsRepository complaintsOptionsRepository,
            IProductsRepository productsRepository
            )
        {

            _complaintsRepository = complaintsRepository;
            _complaintsOptionsRepository = complaintsOptionsRepository;
            _productsRepository = productsRepository;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _complaintsRepository.GetAllAsync();
            return View(model);
        }


        public async Task<ActionResult> Details(int id)
        {
            var model = await _complaintsRepository.GetAsync(id);

            return View(model);
        }



        public async Task<ActionResult> Create()
        {
            var model = new TaskResult<ComplaintsDto>();
            var options = await _complaintsOptionsRepository.GetAllAsync();
            TempData["options"] = options.Data;
            var products = await _productsRepository.GetAllAsync();
            TempData["products"] = products.Data;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ComplaintsDto Data)
        {

            ApplicationUser user = System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(System.Web.HttpContext.Current.User.Identity.GetUserId());

            //Data.CustomersId = user.Id;
         
            Data.StatusId = (int)ComplaintsManagement.Infrastructure.Helpers.Constants.StatusConstants.SOMETIDO;
            var newModel = await _complaintsRepository.SaveAsync(Data);
            var options = await _complaintsOptionsRepository.GetAllAsync();
            TempData["options"] = options.Data;
            var products = await _productsRepository.GetAllAsync();
            TempData["products"] = products.Data;
            return View(newModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index));
            }

            var model = await _complaintsRepository.GetAsync(id);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ComplaintsDto Data)
        {

            if (!ModelState.IsValid)
            {
                return View(new TaskResult<ComplaintsDto> { Data = Data });
            }

            var model = await _complaintsRepository.UpdateAsync(Data);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var deleteResult = await _complaintsRepository.DeleteAsync(Id);
            ViewData["Message"] = deleteResult.Message;
            ViewData["Success"] = deleteResult.Success;
            return RedirectToAction(nameof(this.Index));
        }
    }
}