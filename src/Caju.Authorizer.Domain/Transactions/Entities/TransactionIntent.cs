using Caju.Authorizer.Domain.Transactions.Events;
using Caju.Authorizer.Domain.Transactions.ValueObjects;
using DDD.Core.DomainObjects;

namespace Caju.Authorizer.Domain.Transactions.Entities
{
    public class TransactionIntent
        : Entity<TransactionIntentId>
    {
        public Transaction Transaction { get; } = null!;

        public Guid ConcurrencyStamp { get; set; }

        public bool Authorized { get; set; }

        public string Message { get; set; }

        public string MetaData { get; set; }

        private TransactionIntent()
        {
            // For EF Only.
        }

        private TransactionIntent(TransactionIntentId id,
                                  Transaction transaction,
                                  Guid accountStamp,
                                  bool authorized,
                                  string message,
                                  string metaData)
            : base(id)
        {
            Transaction = transaction;
            ConcurrencyStamp = accountStamp;
            Authorized = authorized;
            Message = message;
            MetaData = metaData;

            AddDomainEvent(new TransactionIntentCreatedEvent(this));
        }

        public static TransactionIntent Create(Transaction transaction,
                                               Guid AccountStamp,
                                               bool authorized,
                                               string message,
                                               string metaData)
        {
            return new TransactionIntent(TransactionIntentId.Create(),
                                         transaction,
                                         AccountStamp,
                                         authorized,
                                         message,
                                         metaData);
        }
    }
}
