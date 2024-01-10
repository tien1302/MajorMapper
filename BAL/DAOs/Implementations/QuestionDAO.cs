using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Accounts;
using BAL.DTOs.Questions;
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
    public class QuestionDAO : IQuestionDAO
    {
        private QuestionRepository _questionRepository;
        private IMapper _mapper;

        public QuestionDAO(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = (QuestionRepository)questionRepository;
            _mapper = mapper;
        }

        public void Create(CreateQuestion create)
        {
            try
            {
                Question question = new Question()
                {
                    PersonalityTypeId = create.PersonalityTypeId,
                    Type = 1,
                    Description = create.Description,
                    CreateDateTime = DateTime.Now,
                    Status = "Processing"
                };

                _questionRepository.Insert(question);
                _questionRepository.Commit();
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
                Question existedQuestion = _questionRepository.GetByID(key);
                if (existedQuestion == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedQuestion.Status = "InActive";
                _questionRepository.Update(existedQuestion);
                _questionRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Láy list question có trạng thái "Processing"
        public List<GetQuestion> GetProcessing()
        {
            try
            {
                List<GetQuestion> listQuestion = _mapper.Map<List<GetQuestion>>(_questionRepository.Get(filter: q => q.Status == "Processing", includeProperties: "PersonalityType").ToList());
                return listQuestion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetQuestion> GetAll()
        {
            try
            {
                List<GetQuestion> listQuestion = _mapper.Map<List<GetQuestion>>(_questionRepository.Get(filter: q => q.Status != "Inactive", includeProperties: "PersonalityType").ToList());
                return listQuestion;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetQuestion Get(int key)
        {
            try
            {
                Question question = this._questionRepository.GetByID(key);
                if (question == null)
                {
                    throw new Exception("question Id không tồn tại.");
                }
                return this._mapper.Map<GetQuestion>(question);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateQuestion update)
        {
            try
            {
                Question existedQuestion = _questionRepository.GetByID(key);
                if (existedQuestion == null)
                {
                    throw new Exception("Question Id does not exist in the system.");
                }

                existedQuestion.Status = update.Status;
                _questionRepository.Update(existedQuestion);
                _questionRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
