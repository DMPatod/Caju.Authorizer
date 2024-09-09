using DDD.Core.DomainObjects;

namespace Caju.Authorizer.Domain.Transactions.ValueObjects
{
    public class TransactionId : ValueObject
    {
        public Guid Value { get; set; }

        private TransactionId(Guid value)
        {
            Value = value;
        }

        public static TransactionId Create(Guid value)
        {
            return new TransactionId(value);
        }

        public static TransactionId Create()
        {
            return new TransactionId(Guid.NewGuid());
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
