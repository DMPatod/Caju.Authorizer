using Caju.Authorizer.Domain.Accounts.ValueObjects;
using Caju.Authorizer.Domain.Transactions;
using Caju.Authorizer.Domain.Transactions.Entities;
using DDD.Core.DomainObjects;
using Newtonsoft.Json;

namespace Caju.Authorizer.Domain.Accounts
{
    public class Account : AggregateRoot<AccountId>
    {
        public double AmountFood { get; set; }

        public double AmountMeal { get; set; }

        public double AmountCash { get; set; }

        public double AmountTotal => AmountFood + AmountMeal + AmountCash;

        public Guid ConcurrencyStamp { get; set; } = Guid.NewGuid();

        private Account()
        {
            // For EF only.
        }

        internal Account(AccountId id, double amountFood, double amountMeal, double amountCash)
            : base(id)
        {
            AmountFood = amountFood;
            AmountMeal = amountMeal;
            AmountCash = amountCash;
        }

        public static Account Create(double amountFood, double amountMeal, double amountCash)
        {
            return new Account(AccountId.Create(), amountFood, amountMeal, amountCash);
        }

        public TransactionIntent Authorize(Transaction transaction)
        {
            if (AmountTotal < transaction.Amount)
            {
                return TransactionIntent.Create(transaction, ConcurrencyStamp, false, "Insufficient funds", JsonConvert.SerializeObject((transaction, this)));
            }

            switch (transaction.MCC)
            {
                case "5411":
                case "5412":
                    if (AmountFood < transaction.Amount)
                    {
                        if (AmountCash < transaction.Amount)
                        {
                            return TransactionIntent.Create(transaction, ConcurrencyStamp, false, "Insufficient funds", JsonConvert.SerializeObject((transaction, this)));
                        }
                        return TransactionIntent.Create(transaction, ConcurrencyStamp, true, "Insufficient Food funds, transaction processed with cash", JsonConvert.SerializeObject((transaction, this)));
                    }
                    return TransactionIntent.Create(transaction, ConcurrencyStamp, true, "Transaction processed successful", JsonConvert.SerializeObject((transaction, this)));
                case "5811":
                case "5812":
                    if (AmountMeal < transaction.Amount)
                    {
                        if (AmountCash < transaction.Amount)
                        {
                            return TransactionIntent.Create(transaction, ConcurrencyStamp, false, "Insufficient funds", JsonConvert.SerializeObject((transaction, this)));
                        }
                        return TransactionIntent.Create(transaction, ConcurrencyStamp, true, "Insufficient Meal funds, transaction processed with cash", JsonConvert.SerializeObject((transaction, this)));
                    }
                    return TransactionIntent.Create(transaction, ConcurrencyStamp, true, "Transaction processed successful", JsonConvert.SerializeObject((transaction, this)));
                default:
                    if (AmountCash < transaction.Amount)
                    {
                        return TransactionIntent.Create(transaction, ConcurrencyStamp, false, "Insufficient funds", JsonConvert.SerializeObject((transaction, this)));
                    }
                    return TransactionIntent.Create(transaction, ConcurrencyStamp, true, "Transaction processed successful", JsonConvert.SerializeObject((transaction, this)));
            }
        }

        public void CommitTransaction(Transaction transaction, Guid transactionStamp)
        {
            if (Id.ToString() != transaction.AccountId)
            {
                throw new Exception("Invalid account passed to commit transaction");
            }

            if (ConcurrencyStamp != transactionStamp)
            {
                throw new Exception("Concurrency exception");
            }

            switch (transaction.MCC)
            {
                case "5411":
                case "5412":
                    if (AmountFood < transaction.Amount)
                    {
                        DoFallback(transaction.Amount);
                        break;
                    }
                    AmountFood -= transaction.Amount;
                    break;
                case "5811":
                case "5812":
                    if (AmountMeal < transaction.Amount)
                    {
                        DoFallback(transaction.Amount);
                        break;
                    }
                    AmountMeal -= transaction.Amount;
                    break;
                default:
                    if (AmountCash < transaction.Amount)
                    {
                        throw new Exception("Insufficient funds");
                    }
                    AmountCash -= transaction.Amount;
                    break;
            }

            ConcurrencyStamp = Guid.NewGuid();
        }

        private void DoFallback(double amount)
        {
            if (AmountCash < amount)
            {
                throw new Exception("Insufficient funds");
            }
            AmountCash -= amount;
        }
    }
}
