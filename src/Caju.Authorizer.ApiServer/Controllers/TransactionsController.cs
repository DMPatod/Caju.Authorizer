using Caju.Authorizer.ApiServer.Contracts.Transactions;
using Caju.Authorizer.Application.Transactions;
using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;
using Microsoft.AspNetCore.Mvc;

namespace Caju.Authorizer.ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IMessageHandler _messageHandler;

        public TransactionsController(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new TransactionFindAllCommand();
            var result = await _messageHandler.SendAsync(command, CancellationToken.None);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionPostRequest request)
        {
            var command = new TransactionCreateCommand(request.Account, request.Amount, request.Merchant, request.Mcc);
            var result = await _messageHandler.SendAsync(command, CancellationToken.None);
            if (result.Authorized)
            {
                return Ok(new { Code = "00" });
            }
            return Ok(new { Code = "51" });
        }
    }
}
