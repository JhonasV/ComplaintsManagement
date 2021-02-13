using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ComplaintsManagement.UI.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {

        private readonly ICustomersRepository _customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        {     
            _customersRepository = customersRepository;
        }

        public async Task<ActionResult> Index()
        {
            var model = await _customersRepository.GetAllAsync();     
            return View(model);
        }


        public async Task<ActionResult> Details(int id)
        {
            var model = await _customersRepository.GetAsync(id);
           
            return View(model);
        }



        public ActionResult Create()
        {
            var model = new TaskResult<CostumersDto>();

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CostumersDto Data)
        {
            if (!ModelState.IsValid)
            {
                return View(Data);
            }

            var newModel = await _customersRepository.SaveAsync(Data);

            return View(newModel);
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Edit(CostumersDto costumersDto)
        {

            if (!ModelState.IsValid)
            {
                return View(new TaskResult<CostumersDto> { Data = costumersDto });
            }

           var model = await _customersRepository.UpdateAsync(costumersDto);
           return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _customersRepository.DeleteAsync(id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}
