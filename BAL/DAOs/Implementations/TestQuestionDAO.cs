using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.TestQuestions;
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
    public class TestQuestionDAO : ITestQuestionDAO
    {
        private TestQuestionRepository _testQuestionRepository;
        private TestRepository _testRepository;
        private QuestionRepository _questionRepository;
        private PersonalityTypeRepository _personalityTypeRepository;
        private IMapper _mapper;

        public TestQuestionDAO(ITestQuestionRepository testQuestionRepository, ITestRepository testRepository,
            IQuestionRepository questionRepository, IPersonalityTypeRepository personalityTypeRepository, IMapper mapper)
        {
            _testQuestionRepository = (TestQuestionRepository)testQuestionRepository;
            _testRepository = (TestRepository)testRepository;
            _questionRepository = (QuestionRepository)questionRepository;
            _personalityTypeRepository = (PersonalityTypeRepository)personalityTypeRepository;
            _mapper = mapper;
        }

        public void Create(CreateTestQuestion create)
        {
            try
            {
                var checkTestId = _testRepository.GetByID(create.TestId);
                var checkQuestionId = _questionRepository.GetByID(create.QuestionId);
                if (checkTestId == null)
                {
                    throw new Exception("Test Id does not exist in the system.");
                }
                if (checkQuestionId == null)
                {
                    throw new Exception("Question Id does not exist in the system.");
                }

                TestQuestion testQuestion = new TestQuestion()
                {
                    TestId = create.TestId,
                    QuestionId = create.QuestionId,
                    GameData = create.GameData,
                    Status = create.Status,
                };

                _testQuestionRepository.Insert(testQuestion);
                _testQuestionRepository.Commit();
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
                TestQuestion existedTestQuestion = _testQuestionRepository.GetByID(key);
                if (existedTestQuestion == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                _testQuestionRepository.Delete(key);
                _testQuestionRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetTestQuestion Get(int key)
        {
            try
            {
                List<GetTestQuestion> listTestQuestion = _mapper.Map<List<GetTestQuestion>>(_testQuestionRepository.Get().ToList());
                TestQuestion testQuestion = _testQuestionRepository.GetByID(key);

                if (testQuestion == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetTestQuestion result = listTestQuestion.FirstOrDefault(p => p.TestId == testQuestion.TestId 
                                                                    && p.QuestionId == testQuestion.QuestionId);
                return _mapper.Map<GetTestQuestion>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetTestQuestion> GetAll()
        {
            try
            {
                List<GetTestQuestion> listTestQuestion = _mapper.Map<List<GetTestQuestion>>(_testQuestionRepository.Get().ToList());
                return listTestQuestion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateTestQuestion update)
        {
            try
            {
                var checkTestId = _testRepository.GetByID(update.TestId);
                var checkQuestionId = _questionRepository.GetByID(update.QuestionId);
                if (checkTestId == null)
                {
                    throw new Exception("Test Id does not exist in the system.");
                }
                if (checkQuestionId == null)
                {
                    throw new Exception("Question Id does not exist in the system.");
                }

                TestQuestion existedTestQuestion = _testQuestionRepository.GetByID(key);
                if (existedTestQuestion == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedTestQuestion.TestId = update.TestId;
                existedTestQuestion.QuestionId = update.QuestionId;
                existedTestQuestion.GameData = update.GameData;
                existedTestQuestion.Status = update.Status;
                _testQuestionRepository.Update(existedTestQuestion);
                _testQuestionRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
