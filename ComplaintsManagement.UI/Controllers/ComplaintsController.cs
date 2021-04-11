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
        private readonly IBinnaclesRepository _binnaclesRepository;
        private readonly IDepartmentsRepository _departmentsRepository;
        private readonly IStatusRepository _statusRepository;

        public ComplaintsController(
            IComplaintsRepository complaintsRepository,
            IComplaintsOptionsRepository complaintsOptionsRepository,
            IProductsRepository productsRepository,
            ICustomersRepository customeRepository,
            ITicketTypesRepository ticketTypesRepository,
            IBinnaclesRepository binnaclesRepository,
            IDepartmentsRepository departmentsRepository,
            IStatusRepository statusRepository
            )
        {

            _complaintsRepository = complaintsRepository;
            _complaintsOptionsRepository = complaintsOptionsRepository;
            _productsRepository = productsRepository;
            _customeRepository = customeRepository;
            _ticketTypesRepository = ticketTypesRepository;
            _binnaclesRepository = binnaclesRepository;
            _departmentsRepository = departmentsRepository;
            _statusRepository = statusRepository;
        }

        public async Task<ActionResult> Index()
        {
            TaskResult<List<ComplaintsDto>> model = new TaskResult<List<ComplaintsDto>>();
            if (User.IsInRole(RoleName.ADMIN))
            {
                model = await _complaintsRepository.GetAllAsync();
            }
            
            else if(User.IsInRole(RoleName.AGENT))
            {
                var user = await _customeRepository.GetAsync(User.Identity.GetUserId());
                model = await _complaintsRepository.GetAllAsync();
                model.Data = model.Data.Where(e => e.DepartmentsId == user.Data.DepartmentId).ToList();
            }
            else
            {
                model = await _complaintsRepository.GetAllByUserIdAsync(User.Identity.GetUserId());
            }



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

            var status1 = await _statusRepository.GetByNameAsync(StatusName.COMMITED);
            Data.StatusId = status1.Data.Id;
            var result = await _complaintsRepository.SaveAsync(Data);
            if (result.Success)
            {
                var ticketType = await _ticketTypesRepository.GetAsync(Data.TicketTypesId);
                var binnacle1 = new BinnacleDto
                {
                    ApplicationUserId = Data.UsersId,
                    StatusId = Data.StatusId,
                    Comment = $"La {ticketType.Data.Description} se ha creado con el estado: {StatusName.COMMITED}",
                    ComplaintsId = result.Data.Id

                };
                await _binnaclesRepository.SaveAsync(binnacle1);

                var deparment = await _departmentsRepository.GetAsync(Data.DepartmentsId);
                if (deparment.Success && deparment.Data != null)
                {
                    var status2 = await _statusRepository.GetByNameAsync(StatusName.TRANSFERRED);
                    await _complaintsRepository.UpdateStatusAsync(status2.Data.Id, result.Data.Id);
                    var binnacle2 = new BinnacleDto
                    {
                        ApplicationUserId = Data.UsersId,
                        StatusId = status2.Data.Id,
                        Comment = $"La {ticketType.Data.Description} ha sido transferido al departamento: {deparment.Data.Name}",
                        ComplaintsId = result.Data.Id

                    };

                    await _binnaclesRepository.SaveAsync(binnacle2);

                }
               


            }

            return View(result);

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