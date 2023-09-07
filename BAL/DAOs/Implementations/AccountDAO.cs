using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Accounts;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
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
        private RoleRepository _roleRepo;
        private IMapper _mapper;

        public AccountDAO(IAccountRepository repo, IRoleRepository roleRepo, IMapper mapper)
        {
            _Repo = (AccountRepository)repo;
            _roleRepo = (RoleRepository)roleRepo;
            _mapper = mapper;
        }

        public List<GetAccount> GetAll()
        {
            try
            {
                List<GetAccount> accounts = this._mapper.Map<List<GetAccount>>(this._Repo.Get(includeProperties: "RoleNavigation").ToList());
                return accounts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetAccount Get(int key)
        {
            try
            {
                Account account = this._Repo.GetByID(key);
                if (account == null)
                {
                    throw new Exception("Account Id does not exist in the system.");
                }
                return this._mapper.Map<GetAccount>(account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Create(CreateAccount create)
        {
            try
            {
                Account account = new Account()
                {
                    Name = create.Name,
                    Email = create.Email,
                    Password = create.Password,
                    Gender = create.Gender,
                    DoB = create.DoB,
                    Role = create.Role,
                    Address = create.Address,
                    Phone = create.Phone,
                    Status = create.Status,
                    Turn = create.Turn,
                    CreatedDateTime = DateTime.Now
                };
                this._Repo.Insert(account);
                this._Repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Update(int key, UpdateAccount update)
        {
            try
            {
                Account existedAccount = this._Repo.GetByID(key);
                if (existedAccount == null)
                {
                    throw new Exception("AccountId does not exist in the system.");
                }           
                
                existedAccount.Name = update.Name;
                existedAccount.Email = update.Email;
                existedAccount.Password = update.Password;
                existedAccount.Gender = update.Gender;
                existedAccount.DoB = update.DoB;
                existedAccount.Role = update.Role;
                existedAccount.Address = update.Address;
                existedAccount.Phone = update.Phone;
                existedAccount.Status = update.Status;
                existedAccount.Turn = update.Turn;
                this._Repo.Update(existedAccount);
                this._Repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int key)
        {
            try
            {
                Account existedAccount = this._Repo.GetByID(key);
                if (existedAccount == null)
                {
                    throw new Exception("AccountId does not exist in the system.");
                }
                this._Repo.Delete(key);
                this._Repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
