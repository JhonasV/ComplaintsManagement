using ComplaintsManagement.Infrastructure.DTOs;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace ComplaintsManagement.UI.Controllers
{
    [Authorize]
    public class CustomersController : Controller
    {

        private readonly ICustomersRepository _customersRepository;
        private readonly ICustomersProductsRepository _customersProductsRepository;
        private readonly IProductsRepository _productsRepository;

        public CustomersController(
            ICustomersRepository customersRepository, 
            ICustomersProductsRepository customersProductsRepository,
            IProductsRepository productsRepository
            )
        {     
            _customersRepository = customersRepository;
            _customersProductsRepository = customersProductsRepository;
            _productsRepository = productsRepository;
        }
        #region "Customers"
        public async Task<ActionResult> Index() => View(await _customersRepository.GetAllAsync());
        public async Task<ActionResult> Details(int id) => View(await _customersRepository.GetAsync(id));
        public ActionResult Create() => View(new TaskResult<CustomersDto>());

        [HttpPost]
        public async Task<ActionResult> Create(CustomersDto Data)
        {
            if (!ModelState.IsValid)
            {
                return View(Data);
            }

            var newModel = await _customersRepository.SaveAsync(Data);

            return RedirectToAction(nameof(this.Create), newModel);
        }

        public async Task<ActionResult> Edit(int id) => View(await _customersRepository.GetAsync(id));

        [HttpPost]
        public async Task<ActionResult> Edit(CustomersDto Data)
        {

            if (!ModelState.IsValid)
            {
                return View(new TaskResult<CustomersDto> { Data = Data });
            }

            var model = await _customersRepository.UpdateAsync(Data);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int Id)
        {

            var deleteResult = await _customersRepository.DeleteAsync(Id);

            var model = await _customersRepository.GetAllAsync();

            model.Message = deleteResult.Message;
            model.Success = deleteResult.Success;
            return View(nameof(this.Index), model);

        }
        #endregion
        #region "Customers Products"
   
        public async Task<ActionResult> CustomerProducts(int Id)
        {
            var customer = await _customersRepository.GetAsync(Id);
            TempData["CustomerName"] = $"{customer.Data.Name} {customer.Data.LastName}";
            TempData["Id"] = Id;

            var model = await _customersProductsRepository.GetAllByCustomerIdAsync(Id);

            return View("Products",model);
        }

        public async Task<ActionResult> CustomerProductsCreate(int Id)
        {
            var customer = await _customersRepository.GetAsync(Id);
            TempData["customer"] = customer.Data;
            var products = await _productsRepository.GetAllAsync();
            TempData["products"] = products.Data;

            var model = new TaskResult<CustomersProductsDto>();

            return View("CustomerProductsCreate", model);
        }

        [HttpPost]
        public async Task<ActionResult> CustomerProductsCreate(CustomersDto customer, ProductsDto product)
        {
            var customerProducts = await _customersProductsRepository.GetAllByCustomerIdAsync(customer.Id);
            var customerAlreadyHasProduct = customerProducts.Data.Exists(e => e.ProductsId == product.Id);
            var customerResult = await _customersRepository.GetAsync(customer.Id);
            TempData["customer"] = customerResult.Data;
            if (customerAlreadyHasProduct)
            {
                var productResult = await _productsRepository.GetAllAsync();

                TempData["products"] = productResult.Data;

                var modelResult = new TaskResult<CustomersProductsDto>();
                modelResult.Message = "El cliente ya tiene este producto";
                modelResult.Success = false;
                return View("CustomerProductsCreate", modelResult);
            }


            var relResult = await _customersProductsRepository.SaveAsync(new CustomersProductsDto { ProductsId = product.Id, UsersId = customer.Id });
            var model = await _productsRepository.GetAllAsync();
            TempData["products"] = model.Data;

            return View("CustomerProductsCreate", relResult);
        }
        #endregion
    }
}
