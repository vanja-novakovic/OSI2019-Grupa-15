﻿using Core.Services;
using Core.Services.Interfaces;
using System;

namespace Core.Common
{
    /// <summary>
    /// Factory methods and singleton pattern used
    /// </summary>
    public class ServicesFactory
    {
        private static ServicesFactory instance = new ServicesFactory();

        private ServicesFactory() { }
        public static ServicesFactory GetInstance()
        {
            return instance;
        }

        public IAccountService CreateIAccountService()
        {
            return new AccountService();
        }
    }
}
