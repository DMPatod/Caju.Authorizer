using Caju.Authorizer.Domain.Transactions.ValueObjects;
using DDD.Core.DomainObjects;

namespace Caju.Authorizer.Domain.Transactions
{
    public class Transaction : AggregateRoot<TransactionId>
    {
        public string Account { get; set; }

        public double Amount { get; set; }

        public string Merchant { get; set; }

        public string MCC { get; set; }

        private Transaction()
        {
            // For EF only.
        }

        private Transaction(TransactionId id, string account, double amount, string merchant, string mcc)
            : base(id)
        {
            Account = account;
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
