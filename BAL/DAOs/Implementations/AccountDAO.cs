using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Accounts;
using DAL.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Implementations
{
    public class AccountDAO : IAccountDAO
    {
        private AccountRepository _Repo;
        private IMapper _mapper;

        public AccountDAO(AccountRepository repo, IMapper mapper)
        {
            _Repo = repo;
            _mapper = mapper;
        }
        public List<GetAccount> GetAll()
        {
            try
            {
                List<GetAccount> Accounts = this._mapper.Map<List<GetAccount>>(this._Repo.Get().ToList());
                return Accounts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
