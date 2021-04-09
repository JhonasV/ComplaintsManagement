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
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentsRepository _departmentsRepository;

        public DepartmentsController(IDepartmentsRepository departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _departmentsRepository.GetAllAsync();
            return View(model);
        }


        public async Task<ActionResult> Details(int id)
        {
            var model = await _departmentsRepository.GetAsync(id);

            return View(model);
        }



        public ActionResult Create()
        {
            var model = new TaskResult<DepartmentsDto>();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(DepartmentsDto Data)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var newModel = await _departmentsRepository.SaveAsync(Data);

            return View(newModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index));
            }

            var model = await _departmentsRepository.GetAsync(id);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DepartmentsDto Data)
        {

            if (!ModelState.IsValid)
            {
                return View(new TaskResult<DepartmentsDto> { Data = Data });
            }

            var model = await _departmentsRepository.UpdateAsync(Data);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var deleteResult = await _departmentsRepository.DeleteAsync(Id);
            ViewData["Message"] = deleteResult.Message;
            ViewData["Success"] = deleteResult.Success;
            return RedirectToAction(nameof(this.Index));
        }
    }
}