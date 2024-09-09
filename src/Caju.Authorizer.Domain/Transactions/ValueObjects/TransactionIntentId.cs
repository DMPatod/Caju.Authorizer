using DDD.Core.DomainObjects;

namespace Caju.Authorizer.Domain.Transactions.ValueObjects
{
    public class TransactionIntentId : ValueObject
    {
        public Guid Value { get; set; }

        private TransactionIntentId(Guid value)
        {
            Value = value;
        }

        public static TransactionIntentId Create(Guid value)
        {
            return new TransactionIntentId(value);
        }

        public static TransactionIntentId Create()
        {
            return new TransactionIntentId(Guid.NewGuid());
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
