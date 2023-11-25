using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Tests;
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
    public class TestDAO : ITestDAO
    {
        private TestRepository _testRepository;
        private AccountRepository _accountRepository;
        private IMapper _mapper;

        public TestDAO(ITestRepository testRepository, IAccountRepository accountRepository, IMapper mapper)
        {
            _testRepository = (TestRepository)testRepository;
            _accountRepository = (AccountRepository)accountRepository;
            _mapper = mapper;
        }

        public void Create(CreateTest create)
        {
            try
            {
                var checkUserId = _accountRepository.GetByID(create.UserId);
                if (checkUserId == null)
                {
                    throw new Exception("User Id does not exist in the system.");
                }

                Test test = new Test()
                {
                    UserId = create.UserId,
                    StatusGame = create.Status,
                    CreateDateTime = DateTime.Now,
                };
                _testRepository.Insert(test);
                _testRepository.Commit();
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
                Test existedTest = _testRepository.GetByID(key);
                if (existedTest == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                _testRepository.Delete(key);
                _testRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetTest Get(int key)
        {
            try
            {
                List<GetTest> listTest = _mapper.Map<List<GetTest>>(_testRepository.Get().ToList());
                Test test = _testRepository.GetByID(key);

                if (test == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetTest result = listTest.FirstOrDefault(p => p.Id == test.Id);
                return _mapper.Map<GetTest>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetTest> GetAll()
        {
            try
            {
                List<GetTest> listTest = _mapper.Map<List<GetTest>>(_testRepository.Get().ToList());
                return listTest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateTest update)
        {
            try
            {
                var checkUserId = _accountRepository.GetByID(update.UserId);
                if (checkUserId == null)
                {
                    throw new Exception("User Id does not exist in the system.");
                }

                Test existedTest = _testRepository.GetByID(key);
                if (existedTest == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedTest.UserId = update.UserId;
                existedTest.StatusGame = update.Status;
                _testRepository.Update(existedTest);
                _testRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
