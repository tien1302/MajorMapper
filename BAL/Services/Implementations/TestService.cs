using AutoMapper;
using BAL.Services.Interfaces;
using BAL.DTOs.Tests;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAL.DTOs.TestResults;
using BAL.DTOs.Scores;

namespace BAL.Services.Implementations
{
    public class TestService : ITestService
    {
        private TestRepository _testRepository;
        private AccountRepository _accountRepository;
        private ScoreRepository _scoreRepository;
        private IMapper _mapper;

        public TestService(ITestRepository testRepository, IAccountRepository accountRepository, IScoreRepository scoreRepository, IMapper mapper)
        {
            _testRepository = (TestRepository)testRepository;
            _accountRepository = (AccountRepository)accountRepository;
            _scoreRepository = (ScoreRepository)scoreRepository;
            _mapper = mapper;
        }

        public void Create(CreateTest create)
        {
            try
            {
                var checkUserId = _accountRepository.GetByID(create.PlayerId);
                if (checkUserId == null)
                {
                    throw new Exception("User Id does not exist in the system.");
                }

                Test test = new Test()
                {
                    PlayerId = create.PlayerId,
                    StatusGame = false,
                    StatusPayment = false,
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

        public void Update(int key)
        {
            try
            {
                Test existedTest = _testRepository.GetByID(key);
                if (existedTest == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedTest.StatusGame = true;
                _testRepository.Update(existedTest);
                _testRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetTest> GetTestbyAccountId(int key)
        {
            try
            {
                List<GetTest> listTest = _mapper.Map<List<GetTest>>(_testRepository.Get(filter: x => x.PlayerId == key && x.StatusPayment == true, includeProperties: "TestResults").ToList());
                foreach (var test in listTest)
                {
                    foreach (var item in test.getTestResults)
                    {
                        List<GetScore> listScores = _mapper.Map<List<GetScore>>(_scoreRepository.Get(filter: x => x.TestResultId == item.Id, includeProperties: "PersonalityType").ToList());
                        item.getScores = listScores;
                    }
                }
                Account account = _accountRepository.GetByID(key);
                if (account == null)
                {
                    throw new Exception("Account Id không tồn tại.");
                }
                return (listTest);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
