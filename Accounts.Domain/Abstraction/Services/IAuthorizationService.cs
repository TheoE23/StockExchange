﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounts.Domain.Abstraction.Services
{
    public interface IAuthorizationService
    {
        public bool Authorize(string token, string requiredRole);
    }
}