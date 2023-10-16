﻿using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Authentications;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
                    CreateDateTime = DateTime.Now
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

        public GetAccount Login(AuthenticationAccount authenAccount, JwtAuth jwtAuth)
        {
            try
            {
                Account existedAccount = this._Repo.Get(x => x.Email == authenAccount.Email && x.Password.Equals(authenAccount.Password))
                                             .SingleOrDefault();
                if (existedAccount == null)
                {
                    throw new Exception("Email or Password is in valid.");
                }
                GetAccount getAccount = this._mapper.Map<GetAccount>(existedAccount);
                //GenerateToken
                switch (existedAccount.Role)
                {
                    case 1:
                        {
                            getAccount.RoleName = "Admin";
                            break;
                        }
                    case 2:
                        {
                            getAccount.RoleName = "Consultant";
                            break;
                        }
                    case 3:
                        {
                            getAccount.RoleName = "User";
                            break;
                        }
                }


                return GenerateToken(getAccount, jwtAuth);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private GetAccount GenerateToken(GetAccount getAccount, JwtAuth jwtAuth)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtAuth.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new ClaimsIdentity(new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, getAccount.Id.ToString()),
                 new Claim(JwtRegisteredClaimNames.Email, getAccount.Email),
                 new Claim(JwtRegisteredClaimNames.Name, getAccount.Name),
                 new Claim("Role", getAccount.RoleName),
                 new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
             });

                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = credentials,
                };

                var token = jwtTokenHandler.CreateToken(tokenDescription);
                string accessToken = jwtTokenHandler.WriteToken(token);

                getAccount.AccessToken = accessToken;

                return getAccount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
