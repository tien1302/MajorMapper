﻿using AutoMapper;
using BAL.Services.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Authentications;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private AccountRepository _Repo;
        private RoleRepository _roleRepo;
        private IMapper _mapper;

        public AccountService(IAccountRepository repo, IRoleRepository roleRepo, IMapper mapper)
        {
            _Repo = (AccountRepository)repo;
            _roleRepo = (RoleRepository)roleRepo;
            _mapper = mapper;
        }

        public List<GetAccount> GetAll()
        {
            try
            {
                List<GetAccount> accounts = this._mapper.Map<List<GetAccount>>(this._Repo.Get(filter: a => a.Status == true && a.Email != "admin@gmail.com", includeProperties: "Role").ToList());
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
                    throw new Exception("Account Id không tồn tại.");
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
                var checkEmail = this._Repo.Get(filter: a => a.Email == create.Email && a.Status == true).FirstOrDefault();
                if (checkEmail != null)
                {
                    throw new Exception("Email đã tồn tại.");
                }

                Account account = new Account()
                {
                    Name = create.Name,
                    Email = create.Email,
                    Password = create.Password,
                    Gender = create.Gender,
                    DoB = create.DoB,
                    RoleId = create.RoleId,
                    Address = create.Address,
                    Phone = create.Phone,
                    Status = true,
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
                var checkEmail = this._Repo.Get(filter: a => a.Email == update.Email && a.Status == true && a.Id == key).FirstOrDefault();
                Account existedAccount = this._Repo.GetByID(key);
                if (checkEmail == null)
                {
                    var checkEmail2 = this._Repo.Get(filter: a => a.Email == update.Email && a.Status == true).FirstOrDefault();
                    if (checkEmail2 != null)
                    {
                        throw new Exception("Email đã tồn tại.");
                    }
  
                }
                
                if (existedAccount == null)
                {
                    throw new Exception("Account Id không tồn tại.");
                }           
                
                existedAccount.Name = update.Name;
                existedAccount.Email = update.Email;
                existedAccount.Gender = update.Gender;
                existedAccount.DoB = update.DoB;
                existedAccount.Address = update.Address;
                existedAccount.Phone = update.Phone;
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
                    throw new Exception("Account Id không tồn tại.");
                }
                existedAccount.Status = false;
                this._Repo.Update(existedAccount);
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
                Account existedAccount = this._Repo.Get(x => x.Email == authenAccount.Email && x.Password.Equals(authenAccount.Password)&& x.Status == true)
                                             .SingleOrDefault();
                if (existedAccount == null)
                {
                    throw new Exception("Email hoặc mật khẩu không đúng. Vui lòng nhập lại.");
                }
                GetAccount getAccount = this._mapper.Map<GetAccount>(existedAccount);
                //GenerateToken
                switch (existedAccount.RoleId)
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
                            getAccount.RoleName = "Player";
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
        public GetAccount LoginGoogle(AuthenticationAccountGoogle authenAccount, JwtAuth jwtAuth)
        {
            try
            {
                Account existedAccount = this._Repo.Get(x => x.Email == authenAccount.Email&& x.Status == true).SingleOrDefault();
                if (existedAccount == null)
                {
                    throw new Exception("Email không tồn tại.");
                }

                GetAccount getAccount = this._mapper.Map<GetAccount>(existedAccount);
                //GenerateToken
                switch (existedAccount.RoleId)
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
                            getAccount.RoleName = "Player";
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
                    Expires = DateTime.UtcNow.AddHours(4),
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

        public void ResetPassword(ResetPassword reset)
        {
            try
            {
                Account existedAccount = this._Repo.Get(x => x.Id == reset.Id && x.Password.Equals(reset.OldPassword))
                                              .SingleOrDefault();
                if (existedAccount == null)
                {
                    throw new Exception("Mặt khẩu cũ không đúng.");
                }

                existedAccount.Password = reset.Password;
                this._Repo.Update(existedAccount);
                this._Repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
