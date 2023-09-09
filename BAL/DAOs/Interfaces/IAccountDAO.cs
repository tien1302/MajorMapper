using BAL.DTOs.Accounts;
using BAL.DTOs.Authentications;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IAccountDAO
    {
        public List<GetAccount> GetAll();
        public GetAccount Get(int key);
        public void Create(CreateAccount create);
        public void Update(int key, UpdateAccount update);
        public void Delete(int key);
        public GetAccount Login(AuthenticationAccount authenAccount, JwtAuth jwtAuth);
    }
}
