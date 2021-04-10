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
    [Authorize]
    public class ComplaintsController : Controller
    {
        private readonly IComplaintsRepository _complaintsRepository;
        private readonly IComplaintsOptionsRepository _complaintsOptionsRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly ICustomersRepository _customeRepository;
        private readonly ITicketTypesRepository _ticketTypesRepository;

        public ComplaintsController(
            IComplaintsRepository complaintsRepository,
            IComplaintsOptionsRepository complaintsOptionsRepository,
            IProductsRepository productsRepository,
            ICustomersRepository customeRepository,
            ITicketTypesRepository ticketTypesRepository
            )
        {

            _complaintsRepository = complaintsRepository;
            _complaintsOptionsRepository = complaintsOptionsRepository;
            _productsRepository = productsRepository;
            _customeRepository = customeRepository;
            _ticketTypesRepository = ticketTypesRepository;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _complaintsRepository.GetAllAsync();
            return View(model);
        }


        public async Task<ActionResult> Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction(nameof(this.Index));
            }

            var model = await _complaintsRepository.GetAsync(id);

            return View(model);
        }



        public async Task<ActionResult> Create()
        {
            var model = new TaskResult<ComplaintsDto>();
            var options = await _complaintsOptionsRepository.GetAllAsync();
            ViewBag.ComplaintsOptions = options.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var products = await _productsRepository.GetAllAsync();
            ViewBag.Products = products.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var customers = await _customeRepository.GetAllAsync();
            ViewBag.Customers = customers.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = $"{e.Name} {e.LastName} ({e.Email})" }).ToList();
            var ticketTypes = await _ticketTypesRepository.GetAllAsync();
            ViewBag.TicketTypes = ticketTypes.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Description }).ToList();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ComplaintsDto Data)
        {
            Data.StatusId = (int)Infrastructure.Helpers.Constants.StatusConstants.SOMETIDO;
            var department = await _complaintsOptionsRepository.GetAsync(Data.ComplaintsOptionsId);
            Data.DepartmentsId = department.Data.DepartmentsId.GetValueOrDefault();

            var options = await _complaintsOptionsRepository.GetAllAsync();
            ViewBag.ComplaintsOptions = options.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var products = await _productsRepository.GetAllAsync();
            ViewBag.Products = products.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var customers = await _customeRepository.GetAllAsync();
            ViewBag.Customers = customers.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = $"{e.Name} {e.LastName} ({e.Email})" }).ToList();
            var ticketTypes = await _ticketTypesRepository.GetAllAsync();
            ViewBag.TicketTypes = ticketTypes.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Description }).ToList();

            //if (Data.Type == Infrastructure.Helpers.Constants.ComplaintClaimType.COMPLAINT)
            //{
            //    return View(await _complaintsRepository.SaveAsync(Data));
            //}


            return View(await _complaintsRepository.SaveAsync(Data));

        }

        public async Task<ActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index));
            }

            var model = await _complaintsRepository.GetAsync(id);
            var options = await _complaintsOptionsRepository.GetAllAsync();
            ViewBag.ComplaintsOptions = options.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var products = await _productsRepository.GetAllAsync();
            ViewBag.Products = products.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var customers = await _customeRepository.GetAllAsync();
            ViewBag.Customers = customers.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = $"{e.Name} {e.LastName} ({e.Email})" }).ToList();

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
            var options = await _complaintsOptionsRepository.GetAllAsync();
            ViewBag.ComplaintsOptions = options.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var products = await _productsRepository.GetAllAsync();
            ViewBag.Products = products.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = e.Name }).ToList();
            var customers = await _customeRepository.GetAllAsync();
            ViewBag.Customers = customers.Data.Select(e => new SelectListItem { Value = e.Id.ToString(), Text = $"{e.Name} {e.LastName} ({e.Email})" }).ToList();
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