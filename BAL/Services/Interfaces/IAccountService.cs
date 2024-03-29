﻿using BAL.DTOs.Accounts;
using BAL.DTOs.Authentications;
using BAL.DTOs.TestResults;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IAccountService
    {
        public List<GetAccount> GetAll();
        public GetAccount Get(int key);
        public void Create(CreateAccount create);
        public void Update(int key, UpdateAccount update);
        public void Delete(int key);

        public GetAccount Login(AuthenticationAccount authenAccount, JwtAuth jwtAuth);
        public GetAccount LoginGoogle(AuthenticationAccountGoogle authenAccount, JwtAuth jwtAuth);
        public void ResetPassword(ResetPassword reset);
    }
}
