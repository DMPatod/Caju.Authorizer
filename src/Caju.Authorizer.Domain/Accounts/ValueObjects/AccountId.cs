using DDD.Core.DomainObjects;

namespace Caju.Authorizer.Domain.Accounts.ValueObjects
{
    public class AccountId : ValueObject
    {
        public Guid Value { get; set; }

        private AccountId(Guid value)
        {
            Value = value;
        }

        public static AccountId Create(Guid value)
        {
            return new AccountId(value);
        }

        public static AccountId Create()
        {
            return new AccountId(Guid.NewGuid());
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
