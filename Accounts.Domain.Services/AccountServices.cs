using Accounts.Domain.Abstraction.Services;
using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;
using Accounts.Infrastructure.Repositories;

namespace Accounts.Domain.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ICurrencyConverter _currencyConverter;

        public AccountServices(IAccountRepository accountRepository, ICurrencyConverter currencyConverter)
        {
            _accountRepository = accountRepository;
            _currencyConverter = currencyConverter;
        }

        public async Task FundAccountAsync(int UserID, decimal amount, string currency)
        {
            var account = await _accountRepository.GetByUserIdAsync(UserID);

            if (account == null)
            {
                throw new Exception($"User not found.");
            }

            decimal usdAmount = _currencyConverter.Convert(amount, currency, "USD");

            account.AccountBalance += usdAmount;
            await _accountRepository.UpdateAsync(account);
        }

        public async Task PurchaseStockAsync(int UserID, string stockName, int quantity, decimal currentPrice)
        {
            var account = await _accountRepository.GetByUserIdAsync(UserID);

            if (account == null)
            {
                throw new Exception($"Account not found.");
            }

            decimal totalCost = quantity * currentPrice;

            if (account.AccountBalance < totalCost)
            {
                throw new Exception($"Not enough balance. Current balance: {account.AccountBalance}, total cost: {totalCost}.");
            }

            account.AccountBalance -= totalCost;
            await _accountRepository.UpdateAsync(account);
        }
    }
}
