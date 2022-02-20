using Gringotts.Services.Contracts;
using Gringotts.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;


namespace Gringotts.WebApi.Controllers
{
    [Route("api/v1.0/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

       [HttpPost]
       public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerRequest customerRequest )
       {
            try
            {
                var result = await _customerService.CreateAsync(customerRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
       }

        [HttpGet]
        [Route("{customerId}")]
        public async Task<IActionResult> GetCustomerAsync([FromQuery] int customerId)
        {
            try
            {
                var result = await _customerService.GetByIdAsync(customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }
    }
}
