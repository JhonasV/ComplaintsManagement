using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComplaintsManagement.UI.Controllers
{
    public class RolesController : Controller
    {
        private readonly IRolesRepository _rolesRepository;

        private readonly ICustomersRepository _customerRepository;
        public RolesController(IRolesRepository rolesRepository, ICustomersRepository customerRepository)
        {
            _rolesRepository = rolesRepository;
          
            _customerRepository = customerRepository;
        }


        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }

        public async Task<ActionResult> Index()
        {
            var model = await _rolesRepository.GetAllAsync();
            return View(model);
        }


        public async Task<ActionResult> Details(string id)
        {
            var model = await _rolesRepository.GetAsync(id);

            return View(model);
        }



        public ActionResult Create()
        {
            var model = new TaskResult<RolesDto>();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(RolesDto Data)
        {


            var newModel = await _rolesRepository.SaveAsync(Data);

            return View(newModel);
        }

        public async Task<ActionResult> Edit(string id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index));
            }

            var model = await _rolesRepository.GetAsync(id);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(RolesDto Data)
        {

            if (!ModelState.IsValid)
            {
                return View(new TaskResult<RolesDto> { Data = Data });
            }

            var model = await _rolesRepository.UpdateAsync(Data);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {
            var deleteResult = await _rolesRepository.DeleteAsync(Id);
            ViewData["Message"] = deleteResult.Message;
            ViewData["Success"] = deleteResult.Success;
            return RedirectToAction(nameof(this.Index));
        }


        [HttpGet]
        public async Task<ActionResult> AddUserRole()
        {
            var roles = await _rolesRepository.GetAllAsync();
            ViewBag.Roles = roles.Data.Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList();
           
            var users = await _customerRepository.GetAllAsync();
            ViewBag.Users = users.Data.Select(u => new SelectListItem { Text = u.Email, Value = u.Id }).ToList();
            var taskResult = new TaskResult<UsersRolesDto>();
            return View(taskResult);
        }

        [HttpPost]
        public async Task<ActionResult> AddUserRole(UsersRolesDto Data)
        {
            var result = await UserManager.AddToRoleAsync(Data.UsersId, Data.RoleName);
            var taskResult = new TaskResult<UsersRolesDto>
            {
                Data = Data
            };
            if (result.Succeeded)
            {

                taskResult.Message = "Se agregó el role correctamente";
            }
            else
            {
                taskResult.Success = false;
                taskResult.Message = String.Join(", ", result.Errors);
            }
            var roles = await _rolesRepository.GetAllAsync();
            ViewBag.Roles = roles.Data.Select(r => new SelectListItem { Text = r.Name, Value = r.Name }).ToList();

            var users = await _customerRepository.GetAllAsync();
            ViewBag.Users = users.Data.Select(u => new SelectListItem { Text = u.Email, Value = u.Id }).ToList();
            return View(taskResult);
        }
    }
}