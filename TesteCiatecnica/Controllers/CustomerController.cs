using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TesteCiatecnica.Models.ViewModels;
using TesteCiatecnica.Services;

namespace TesteCiatecnica.Controllers
{
    public class CustomersController : Controller
    {
        private readonly CustomerService _customerService;

        public CustomersController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _customerService.Read());
        }

        public async Task<IActionResult> List()
        {
            try
            {
                var customers = await _customerService.Read();
                return Json(CustomerViewModel.FromCustomer(customers));
            }
            catch (ApplicationException a)
            {
                return BadRequest(a.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // GET: Customers/Read/5
        public async Task<IActionResult> Read(int? id)
        {
            if (id == null)
            {
                BadRequest("Cliente não encontrado");
            }

            try
            {
                var customer = await _customerService.Read(id);
                return Json(CustomerViewModel.FromCustomer(customer));
            }
            catch (ApplicationException a)
            {
                return BadRequest(a.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: Customers/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerViewModel customerViewModel)
        {
            if(customerViewModel is null)
            {
                return BadRequest("Cliente inválido");
            }

            var customer = customerViewModel.ToCustomer();

            try
            {
                var message = await _customerService.Create(customer);
                return Json(message);
            }
            catch(ApplicationException a)
            {
                return BadRequest(a.Message);
            }
            catch (ValidationException v)
            {
                return Ok(new
                {
                    HasValidationErrors = true,
                    ValidationErrors = v.Message,
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CustomerViewModel customerViewModel)
        {
            if (customerViewModel is null)
            {
                return BadRequest("Cliente inválido");
            }

            var customer = customerViewModel.ToCustomer();

            try
            {
                var message = await _customerService.Update(customer);
                return Json(message);
            }
            catch (ApplicationException a)
            {
                return BadRequest(a.Message);
            }
            catch (ValidationException v)
            {
                return Ok(new
                {
                    HasValidationErrors = true,
                    ValidationErrors = v.Message,
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var message = await _customerService.Delete(id);
                return Json(message);
            }
            catch (ApplicationException a)
            {
                return BadRequest(a.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
