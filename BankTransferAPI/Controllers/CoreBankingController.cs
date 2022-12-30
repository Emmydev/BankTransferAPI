using BankTransfer.BLL.Services.CoreBanking;
using BankTransfer.Contracts.Authentication;
using BankTransfer.Contracts.CoreBanking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankTransferAPI.Controllers
{
    [Authorize]
    [Route("/api/v1/core-banking")]
    public class CoreBankingController: Controller
    {
        private readonly ICoreBankingService service;

        public CoreBankingController(ICoreBankingService service)
        {
            this.service = service;
        }
        [HttpGet("banks/{providerId}")]
        public async Task<IActionResult> GetBanks(int providerId)
        {
            var response = await service.GetBankList(providerId);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("providers")]
        public async Task<IActionResult> GetProviders()
        {
            var response = await service.GetProviders();
            if(response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("validateBankAccount")]
        public async Task<IActionResult> ValidateBankAccount([FromBody]ValidateAccountNo request)
        {
            var response = await service.ValidateAccountNumber(request);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpPost("bankTransfer")]
        public async Task<IActionResult> BankTransfer([FromBody]BankTransferRequest request)
        {
            var response = await service.BankTransfer(request);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }
        [HttpGet("transaction")]
        public async Task<IActionResult> Transaction(string transactionReference)
        {
            var response = await service.TransactionStatus(transactionReference);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
