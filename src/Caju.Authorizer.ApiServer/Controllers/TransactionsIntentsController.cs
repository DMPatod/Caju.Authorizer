using Caju.Authorizer.Application.Transactions.TransactionIntents;
using DDD.Core.Handlers.SHS.RD.CGC.Core.DomainEvents;
using Microsoft.AspNetCore.Mvc;

namespace Caju.Authorizer.ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsIntentsController : ControllerBase
    {
        private readonly IMessageHandler _messageHandler;

        public TransactionsIntentsController(IMessageHandler messageHandler)
        {
            _messageHandler = messageHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var command = new TransactionIntentFindAllCommand();
            var result = await _messageHandler.SendAsync(command, CancellationToken.None);
            return Ok(result);
        }
    }
}
