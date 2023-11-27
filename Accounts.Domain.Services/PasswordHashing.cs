using Accounts.Domain.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Accounts.Domain.Services
{
    public class PasswordHashing : IPasswordHashing
    {
        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            string passwordHash = HashPassword(password);
            return passwordHash.Equals(hashedPassword);
        }
    }
}
