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
    [Authorize(Roles = RoleName.ADMIN)]
    public class ComplaintsOptionsController : Controller
    {
        private readonly IComplaintsOptionsRepository _complaintsOptionsRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IDepartmentsRepository _departmentsRepository;

        public ComplaintsOptionsController(IComplaintsOptionsRepository complaintsOptionsRepository, IProductsRepository productsRepository, IDepartmentsRepository departmentsRepository)
        {
            _complaintsOptionsRepository = complaintsOptionsRepository;
            _productsRepository = productsRepository;
            _departmentsRepository = departmentsRepository;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _complaintsOptionsRepository.GetAllAsync();
            return View(model);
        }


        public async Task<ActionResult> Details(int id)
        {
            var model = await _complaintsOptionsRepository.GetAsync(id);

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            var model = new TaskResult<ComplaintsOptionsDto>();
            var products = await _productsRepository.GetAllAsync();
            ViewBag.Products = products.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var departments = await _departmentsRepository.GetAllAsync();
            ViewBag.Departments = departments.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ComplaintsOptionsDto Data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var products = await _productsRepository.GetAllAsync();
            ViewBag.Products = products.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var departments = await _departmentsRepository.GetAllAsync();
            ViewBag.Departments = departments.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var newModel = await _complaintsOptionsRepository.SaveAsync(Data);

            return View(newModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index));
            }

            var model = await _complaintsOptionsRepository.GetAsync(id);
            var products = await _productsRepository.GetAllAsync();
            ViewBag.Products = products.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var departments = await _departmentsRepository.GetAllAsync();
            ViewBag.Departments = departments.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(ComplaintsOptionsDto Data)
        {

            if (!ModelState.IsValid)
            {
                return View(new TaskResult<ComplaintsOptionsDto> { Data = Data });
            }
            var products = await _productsRepository.GetAllAsync();
            ViewBag.Products = products.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var departments = await _departmentsRepository.GetAllAsync();
            ViewBag.Departments = departments.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();

            var model = await _complaintsOptionsRepository.UpdateAsync(Data);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var deleteResult = await _complaintsOptionsRepository.DeleteAsync(Id);
            ViewData["Message"] = deleteResult.Message;
            ViewData["Success"] = deleteResult.Success;
            return RedirectToAction(nameof(this.Index));
        }
    }
}