using Gringotts.Services.Contracts;
using Gringotts.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gringotts.WebApi.Controllers
{
    [Route("api/v1.0/customers/{customerId}/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync(int customerId, [FromBody] CreateAccountRequest accountRequest)
        {
            try
            {
                var result = await _accountService.CreateAsync(customerId, accountRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAccountsAsync(int customerId)
        {
            try
            {
                var result = await _accountService.GetAllAsync(customerId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        [HttpGet]
        [Route("{accountId}")]
        public async Task<IActionResult> GetAccountByIdAsync(int customerId, int accountId)
        {
            try
            {
                var result = await _accountService.GetByIdAsync(customerId, accountId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }
    }
}
