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
    public class StatusController : Controller
    {
        private readonly IStatusRepository _statusRepository;

        public StatusController(IStatusRepository statusRepository)
        {
            _statusRepository = statusRepository;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _statusRepository.GetAllAsync();
            return View(model);
        }


        public async Task<ActionResult> Details(int id)
        {
            var model = await _statusRepository.GetAsync(id);

            return View(model);
        }



        public ActionResult Create()
        {
            var model = new TaskResult<StatusDto>();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(StatusDto Data)
        {


            var newModel = await _statusRepository.SaveAsync(Data);

            return View(newModel);
        }

        public async Task<ActionResult> Edit(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(this.Index));
            }

            var model = await _statusRepository.GetAsync(id);


            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(StatusDto Data)
        {

            if (!ModelState.IsValid)
            {
                return View(new TaskResult<StatusDto> { Data = Data });
            }

            var model = await _statusRepository.UpdateAsync(Data);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {
            var deleteResult = await _statusRepository.DeleteAsync(Id);
            ViewData["Message"] = deleteResult.Message;
            ViewData["Success"] = deleteResult.Success;
            return RedirectToAction(nameof(this.Index));
        }
    }
}