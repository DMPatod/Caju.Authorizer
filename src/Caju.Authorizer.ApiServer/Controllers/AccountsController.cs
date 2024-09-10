using Caju.Authorizer.ApiServer.Contracts.Accounts;
using Caju.Authorizer.Application.Accounts;
using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;
using Microsoft.AspNetCore.Mvc;

namespace Caju.Authorizer.ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMessageHandler _messageHandler;

        public AccountsController(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new AccountFindAllCommand();
            var result = await _messageHandler.SendAsync(command, CancellationToken.None);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountCreateRequest request)
        {
            var command = new AccountCreateCommand(request.AmountFood, request.AmountMeal, request.AmountCash);
            var result = await _messageHandler.SendAsync(command, CancellationToken.None);
            return Ok(result);
        }
    }
}
