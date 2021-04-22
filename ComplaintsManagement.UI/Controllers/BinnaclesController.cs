using ComplaintsManagement.Domain.DTOs;
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
    public class BinnaclesController : Controller
    {
        private readonly IBinnaclesRepository _binnaclesRepository;
        private readonly IComplaintsRepository _complaintsRepository;


        public BinnaclesController(IBinnaclesRepository binnaclesRepository, IComplaintsRepository complaintsRepository)
        {
            _binnaclesRepository = binnaclesRepository;
            _complaintsRepository = complaintsRepository;
            
        }



        // GET: Binnacles
        [HttpGet]
        public async Task<ActionResult> Get(int complaintsId)
        {
            var binnacles = await _binnaclesRepository.GetAllByComplaintsIdAsync(complaintsId);
            return Json(binnacles, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public async Task<ActionResult> Add(BinnacleDto binnacleDto)
        {

            var complaint = await _complaintsRepository.GetAsync(binnacleDto.ComplaintsId.GetValueOrDefault());
            var binnacle = new BinnacleDto
            {
                ApplicationUserId = User.Identity.GetUserId(),
                StatusId = complaint.Data.StatusId,
                Comment = binnacleDto.Comment.ToUpper(),
                ComplaintsId = complaint.Data.Id

            };
            var binnacles = await _binnaclesRepository.SaveAsync(binnacle);
            return Json(binnacles);
        }
    }
}