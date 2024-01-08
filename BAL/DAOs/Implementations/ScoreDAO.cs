using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Scores;
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
    public class ScoreDAO : IScoreDAO
    {
        private ScoreRepository _scoreRepository;
        private PersonalityTypeRepository _personalityTypeRepository;
        private TestResultRepository _testResultRepository;
        private IMapper _mapper;

        public ScoreDAO(IScoreRepository scoreRepository, IPersonalityTypeRepository personalityTypeRepository, ITestResultRepository testResultRepository, IMapper mapper)
        {
            _scoreRepository = (ScoreRepository)scoreRepository;
            _personalityTypeRepository = (PersonalityTypeRepository)personalityTypeRepository;
            _testResultRepository = (TestResultRepository)testResultRepository;
            _mapper = mapper;
        }

        public void Create(CreateScore create)
        {
            try
            {
                var checkTestResultId = _testResultRepository.GetByID(create.TestResultId);
                var checkPersonalityTypeId = _personalityTypeRepository.GetByID(create.PersonalityTypeId);
                if (checkTestResultId == null)
                {
                    throw new Exception("Test Result Id does not exist in the system.");
                }
                if (checkPersonalityTypeId == null)
                {
                    throw new Exception("PersonalityType Id does not exist in the system.");
                }

                Score score = new Score()
                {
                    TestResultId = create.TestResultId,
                    PersonalityTypeId = create.PersonalityTypeId,
                    Result = create.Result,
                };

                _scoreRepository.Insert(score);
                _scoreRepository.Commit();
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
                Score existedScore = _scoreRepository.GetByID(key);
                if (existedScore == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                _scoreRepository.Delete(key);
                _scoreRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetScore Get(int key)
        {
            try
            {
                List<GetScore> listScore = _mapper.Map<List<GetScore>>(_scoreRepository.Get().ToList());
                Score score = _scoreRepository.GetByID(key);

                if (score == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetScore result = listScore.FirstOrDefault(p => p.TestResultId == score.TestResultId && p.PersonalityTypeId == score.PersonalityTypeId);
                return _mapper.Map<GetScore>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetScore> GetAll()
        {
            try
            {
                List<GetScore> listScore = _mapper.Map<List<GetScore>>(_scoreRepository.Get().ToList());
                return listScore;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateScore update)
        {
            try
            {
                var checkTestResultId = _testResultRepository.GetByID(update.TestResultId);
                var checkPersonalityTypeId = _personalityTypeRepository.GetByID(update.PersonalityTypeId);
                if (checkTestResultId == null)
                {
                    throw new Exception("Test Result Id does not exist in the system.");
                }
                if (checkPersonalityTypeId == null)
                {
                    throw new Exception("PersonalityType Id does not exist in the system.");
                }

                Score existedScore = _scoreRepository.GetByID(key);
                if (existedScore == null)
                {
                    throw new Exception("Score Id does not exist in the system.");
                }

                existedScore.TestResultId = update.TestResultId;
                existedScore.PersonalityTypeId = update.PersonalityTypeId;
                existedScore.Result = update.Result;
                _scoreRepository.Update(existedScore);
                _scoreRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
