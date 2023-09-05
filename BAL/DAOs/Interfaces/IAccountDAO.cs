using BAL.DTOs.Accounts;
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
        public void Create(GetAccount create);
        public void Update(int key, GetAccount update);
        public void Delete(int key);
    }
}
