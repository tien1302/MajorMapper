using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.ReviewTests;
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
    public class ReviewTestDAO : IReviewTestDAO
    {
        private ReviewTestRepository _reviewTestRepository;
        private TestResultRepository _testResultRepository;
        private IMapper _mapper;

        public ReviewTestDAO(IReviewTestRepository reviewTestRepository, ITestResultRepository testResultRepository, IMapper mapper)
        {
            _reviewTestRepository = (ReviewTestRepository)reviewTestRepository;
            _testResultRepository = (TestResultRepository)testResultRepository;
            _mapper = mapper;
        }

        public void Create(CreateReviewTest create)
        {
            try
            {
                ReviewTest reviewTest = new ReviewTest()
                {
                    TestResultId = create.TestResultId,
                    Comment = create.Comment,
                    Star = create.Star,
                    CreateDateTime = DateTime.Now,
                };
                _reviewTestRepository.Insert(reviewTest);
                _reviewTestRepository.Commit();
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
                ReviewTest existedReviewTest = _reviewTestRepository.GetByID(key);
                if (existedReviewTest == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                _reviewTestRepository.Delete(key);
                _reviewTestRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetReviewTest Get(int key)
        {
            try
            {
                List<GetReviewTest> listReviewTest = _mapper.Map<List<GetReviewTest>>(_reviewTestRepository.Get().ToList());
                ReviewTest reviewTest = _reviewTestRepository.GetByID(key);

                if (reviewTest == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetReviewTest result = listReviewTest.FirstOrDefault(p => p.Id == reviewTest.Id);
                return _mapper.Map<GetReviewTest>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetReviewTest> GetAll()
        {
            try
            {
                List<GetReviewTest> listReviewTest = _mapper.Map<List<GetReviewTest>>(_reviewTestRepository.Get().ToList());
                return listReviewTest;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
