﻿using ComplaintsManagement.Domain.DTOs;
using ComplaintsManagement.UI.Models;
using ComplaintsManagement.UI.Services.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace ComplaintsManagement.UI.Controllers
{
    [Authorize(Roles = RoleName.ADMIN)]
    public class CustomersController : Controller
    {

        private readonly ICustomersRepository _customersRepository;
        private readonly ICustomersProductsRepository _customersProductsRepository;
        private readonly IProductsRepository _productsRepository;
        private readonly IDepartmentsRepository _departmentRepository;

        public CustomersController(
            ICustomersRepository customersRepository, 
            ICustomersProductsRepository customersProductsRepository,
            IProductsRepository productsRepository,
            IDepartmentsRepository departmentsRepository
            )
        {     
            _customersRepository = customersRepository;
            _customersProductsRepository = customersProductsRepository;
            _departmentRepository = departmentsRepository;
            _productsRepository = productsRepository;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }


        #region "Customers"
        public async Task<ActionResult> Index() => View(await _customersRepository.GetAllAsync(UserManager));
        public async Task<ActionResult> Details(string id) => View(await _customersRepository.GetAsync(id));
        public async Task<ActionResult> Create()
        {
            var departments = await _departmentRepository.GetAllAsync();
            var selectListItems = departments.Data.Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }).ToList();
            selectListItems.Add(new SelectListItem { Text = string.Empty, Value = string.Empty, Selected = true });
            ViewBag.Departments = selectListItems;
            return View(new TaskResult<UsersDto>());
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsersDto Data)
        {
            if (!ModelState.IsValid)
            {
                return View(Data);
            }
            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = departments.Data.Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }).ToList();
            var newModel = await _customersRepository.SaveAsync(Data, UserManager);

            if (newModel.Success)
            {
                return RedirectToAction(nameof(this.Create), newModel);
            }
            
            return View(newModel);
        }

        public async Task<ActionResult> Edit(string id) 
        {
            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = departments.Data.Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }).ToList();
            return View(await _customersRepository.GetAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UsersDto Data)
        {

            if (!ModelState.IsValid)
            {
                return View(new TaskResult<UsersDto> { Data = Data });
            }
            var departments = await _departmentRepository.GetAllAsync();
            ViewBag.Departments = departments.Data.Select(e => new SelectListItem { Text = e.Name, Value = e.Id.ToString() }).ToList();
            var model = await _customersRepository.UpdateAsync(Data);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string Id)
        {

            var deleteResult = await _customersRepository.DeleteAsync(Id);

            var model = await _customersRepository.GetAllAsync();

            model.Message = deleteResult.Message;
            model.Success = deleteResult.Success;
            return View(nameof(this.Index), model);

        }
        #endregion
        #region "Customers Products"
   
        public async Task<ActionResult> CustomerProducts(string Id)
        {
            var customer = await _customersRepository.GetAsync(Id);
            TempData["CustomerName"] = $"{customer.Data.Name} {customer.Data.LastName}";
            TempData["Id"] = Id;

            var model = await _customersProductsRepository.GetAllByCustomerIdAsync(Id);
            return View("Products", model);
        }

        public async Task<ActionResult> CustomerProductsCreate(string Id)
        {
            var customer = await _customersRepository.GetAsync(Id);
            TempData["customer"] = customer.Data;
            var products = await _productsRepository.GetAllAsync();

            ViewBag.Products = products.Data;
            var model = new TaskResult<CustomersProductsDto>();

            return View("CustomerProductsCreate", model);
        }

        [HttpPost]
        public async Task<ActionResult> CustomerProductsCreate(UsersDto customer, ProductsDto product)
        {
            var customerResult = await _customersRepository.GetAsync(customer.Id);
            TempData["customer"] = customerResult.Data;
            
            var productResult = await _productsRepository.GetAllAsync();
            ViewBag.Products = productResult.Data;

            var customerProducts = await _customersProductsRepository.GetAllByCustomerIdAsync(customer.Id);
            var customerAlreadyHasProduct = customerProducts.Data.Exists(e => e.ProductsId == product.Id);
            if (customerAlreadyHasProduct)
            {
                var modelResult = new TaskResult<CustomersProductsDto>
                {
                    Message = "El cliente ya tiene este producto",
                    Success = false
                };
                return View("CustomerProductsCreate", modelResult);
            }

            var customerProduct = new CustomersProductsDto 
            { 
                ProductsId = product.Id,
                ApplicationUserId = customer.Id
            };

            var relResult = await _customersProductsRepository.SaveAsync(customerProduct);
            return View("CustomerProductsCreate", relResult);
        }


        [HttpPost]
        public async Task<ActionResult> CustomerProductDelete(int Id)
        {
            var isDeleted = await _customersProductsRepository.DeleteAsync(Id);
            return View("Products", isDeleted);
        }
        #endregion
    }
}
