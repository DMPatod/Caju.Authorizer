namespace Caju.Authorizer.ApiServer.Contracts.Transactions
{
    public record TransactionCreateRequest(
        string Account,
        double Amount,
        string Merchant,
        string Mcc
        );

}
