using Caju.Authorizer.Domain.Accounts;
using Caju.Authorizer.Domain.Accounts.Repositories;
using DDD.Core.Handlers;
using DDD.Core.Messages;

namespace Caju.Authorizer.Application.Accounts
{
    public record AccountFindAllCommand() : ICommand<ICollection<Account>>;

    public class AccountFindAllCommandHandler : ICommandHandler<AccountFindAllCommand, ICollection<Account>>
    {
        private readonly IAccountRepository _repository;

        public AccountFindAllCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Account>> Handle(AccountFindAllCommand request, CancellationToken cancellationToken)
        {
            return await _repository.FindAsync(cancellationToken);
        }
    }
}
