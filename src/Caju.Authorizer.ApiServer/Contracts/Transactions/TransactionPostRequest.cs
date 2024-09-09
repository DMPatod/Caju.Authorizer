namespace Caju.Authorizer.ApiServer.Contracts.Transactions
{
    public record TransactionPostRequest(
        string Account,
        double Amount,
        string Merchant,
        string Mcc
        );

}
