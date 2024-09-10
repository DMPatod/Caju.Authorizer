## Caju Authorizer

# Features
- Create Account
To create an account it must POST the following json, to "api/accounts":
```json
{
  foodFunds: number,
  mealFunds: number,
  cashFunds: number
}
```
the response will be the account created with the id.
- List Accounts
- Create Transaction
To create a Transaction it must POST the following json, to "api/transactions":
```json
{
  accountId: number,
  amount: number,
  merchant: string,
  mcc: string
}
```
the response will be the status of the transaction. If it was approved or not. No more details
- List Transactions
- List TransactionsIntent

# How to run
To run the project you must have docker and docker-compose installed.
After that, you must run the following command:
```bash
	docker-compose up
```
This command will build the project and run the application on port 8080. The database will be running on port 5432.