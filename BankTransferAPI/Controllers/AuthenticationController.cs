using BankTransfer.BLL.Services.Authentication;
using BankTransfer.Contracts.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankTransferAPI.Controllers
{
    [Route("/api/v1/core-banking")]
    public class AuthenticationController: Controller
    {
        private readonly IIdentityService service;

        public AuthenticationController(IIdentityService service)
        {
            this.service = service;
        }
        [HttpPost("authenticate")]
        public async Task<IActionResult> CreateCandidate([FromBody]LoginDetails request)
        {
            var response = await service.Login(request);
            if (response.IsSuccessful)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
