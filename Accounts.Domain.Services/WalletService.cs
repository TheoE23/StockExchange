using System;
using System.Threading.Tasks;
using Accounts.Domain.Abstraction.Services;
using Accounts.Domain.Entities;
using Accounts.Domain.Interfaces;

namespace Accounts.Domain.Services
{
    public class WalletService : IWalletServices
    {
        private readonly IAccountRepository _accountRepository;

        public WalletService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<bool> DepositAsync(int UserID, decimal Amount)
        {
            try
            {
                
                var userAccount = await _accountRepository.GetByUserIdAsync(UserID);

                if (userAccount == null)
                {
                    return false;
                }

                userAccount.AccountBalance += Amount;
                
                await _accountRepository.UpdateAsync(userAccount);

                return true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during deposit: {ex.Message}");
                return false;
            }
        }
    }
}
