using Caju.Authorizer.Domain.Transactions.Entities;
using Caju.Authorizer.Domain.Transactions.ValueObjects;
using DDD.Core.DomainObjects;

namespace Caju.Authorizer.Domain.Transactions
{
    public class Transaction : AggregateRoot<TransactionId>
    {
        public string AccountId { get; set; }

        public double Amount { get; set; }

        public string Merchant { get; set; }

        public string MCC { get; set; }

        public IReadOnlyCollection<TransactionIntent> TransactionIntents { get; } = [];

        private Transaction()
        {
            // For EF only.
        }

        internal Transaction(TransactionId id, string account, double amount, string merchant, string mcc)
            : base(id)
        {
            AccountId = account;
            Amount = amount;
            Merchant = merchant;
            MCC = mcc;
        }

        public static Transaction Create(string account, double amount, string merchant, string mcc)
        {
            return new Transaction(TransactionId.Create(), account, amount, merchant, mcc);
        }
    }
}
