using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Accounts.Repositories;
using DDD.Core.Handlers;
using DDD.Core.Messages;

namespace Caju.Authorizer.Application.Accounts
{
    public record AccountCreateCommand(double AmountFood, double AmountMeal, double AmountCash) : ICommand<Account>;

    public class AccountCreateCommandHandler : ICommandHandler<AccountCreateCommand, Account>
    {
        private readonly IAccountRepository _repository;

        public AccountCreateCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<Account> Handle(AccountCreateCommand request, CancellationToken cancellationToken)
        {
            var account = Account.Create(request.AmountFood, request.AmountMeal, request.AmountCash);
            await _repository.CreateAsync(account, cancellationToken);
            return account;
        }
    }
}
