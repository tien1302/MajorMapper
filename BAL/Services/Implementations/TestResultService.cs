using AutoMapper;
using BAL.Services.Interfaces;
using BAL.DTOs.Scores;
using BAL.DTOs.TestResults;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implementations
{
    public class TestResultService : ITestResultService
    {
        private TestResultRepository _testResultRepository;
        private ScoreRepository _scoreRepository;
        private IMapper _mapper;

        public TestResultService(ITestResultRepository testResultRepository, IScoreRepository scoreRepository, IMapper mapper)
        {
            _testResultRepository = (TestResultRepository)testResultRepository;
            _scoreRepository = (ScoreRepository)scoreRepository;
            _mapper = mapper;
        }

        public void Create(CreateTestResult create)
        {
            try
            {
                TestResult testResult = new TestResult()
                {
                    TestId = create.TestId,
                    CreateDateTime = DateTime.Now,
                };

                _testResultRepository.Insert(testResult);
                _testResultRepository.Commit();
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
                TestResult existedTestResult = _testResultRepository.GetByID(key);
                if (existedTestResult == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                _testResultRepository.Delete(key);
                _testResultRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetTestResult Get(int key)
        {
            try
            {
                List<GetTestResult> listTestResult = _mapper.Map<List<GetTestResult>>(_testResultRepository.Get(includeProperties: "Scores,Test").ToList());
                TestResult testResult = _testResultRepository.GetByID(key);

                if (testResult == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetTestResult result = listTestResult.FirstOrDefault(p => p.Id == testResult.Id);
                return _mapper.Map<GetTestResult>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetTestResult> GetAll()
        {
            try
            {
                List<GetTestResult> listTestResult = _mapper.Map<List<GetTestResult>>(_testResultRepository.Get().ToList());
                return listTestResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
