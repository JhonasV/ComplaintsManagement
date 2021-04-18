using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Services.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ComplaintsManagement.UI.Controllers
{
    public class ServiceRateController : Controller
    {
        private readonly IServiceRateRepository _serviceRateRepository;
        private readonly IBinnaclesRepository _binnaclesRepository;
        private readonly IComplaintsRepository _complaintsRepository;

        public ServiceRateController(IServiceRateRepository serviceRateRepository, IBinnaclesRepository binnaclesRepository, IComplaintsRepository complaintsRepository)
        {
            _serviceRateRepository = serviceRateRepository;
            _binnaclesRepository = binnaclesRepository;
            _complaintsRepository = complaintsRepository;
        }
        [HttpPost]
        public async Task<ActionResult> Create(ServiceRateDto ServiceRate)
        {
            var serviceRateExists = await _serviceRateRepository.GetByComplaintsIdAsync(ServiceRate.ComplaintsId);

            if(serviceRateExists.Data == null)
            {
                var saved = await _serviceRateRepository.SaveAsync(ServiceRate);
            }
            else
            {
                serviceRateExists.Data.Rate = ServiceRate.Rate;
                var removedPreviousRate = await _serviceRateRepository.UpdateAsync(serviceRateExists.Data);
            }

            var complaints = await _complaintsRepository.GetAsync(ServiceRate.ComplaintsId);
            var binnacle = new BinnacleDto
            {
                ComplaintsId = ServiceRate.ComplaintsId,
                StatusId = complaints.Data.StatusId,
                Comment = $"SE HA PUNTUADO EL TICKET COMO: {ServiceRate.Rate}",
                ApplicationUserId = User.Identity.GetUserId()
            };

            await _binnaclesRepository.SaveAsync(binnacle);

            return RedirectToAction(nameof(ComplaintsController.Details), "Complaints", new { id = ServiceRate.ComplaintsId });
        }
    }
}