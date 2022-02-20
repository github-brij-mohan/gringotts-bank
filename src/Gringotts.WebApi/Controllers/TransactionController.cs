using Gringotts.Services.Contracts;
using Gringotts.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Gringotts.WebApi.Controllers
{
    [Route("api/v1.0/customers/{customerId}/accounts/{accountNumber}/transactions")]
    [ApiController]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionAsync(int customerId, int accountNumber, [FromBody] CreateTransactionRequest createTransactionRequest)
        {
            try
            {
                var result = await _transactionService.CreateAsync(customerId, accountNumber, createTransactionRequest);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionsAsync(int customerId, int accountNumber, 
                                                                 [FromQuery] DateTime? start, [FromQuery] DateTime? end)
        {
            try
            {
                start ??= DateTime.MinValue;
                end ??= DateTime.Now;
                var result = await _transactionService.GetAllAsync(customerId, accountNumber, start.Value, end.Value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        [HttpGet]
        [Route("{transactionId}")]
        public async Task<IActionResult> GetAccountByIdAsync(int customerId, int accountNumber, int transactionId)
        {
            try
            {
                var result = await _transactionService.GetByIdAsync(customerId, accountNumber, transactionId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }
    }
}
