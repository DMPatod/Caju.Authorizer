using Caju.Authorizer.Domain.Transactions.Entities;
using DDD.Core.Messages;

namespace Caju.Authorizer.Domain.Transactions.Events
{
    public record TransactionIntentCreatedEvent(TransactionIntent Intent) : IEvent;
}
