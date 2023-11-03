using AutoMapper;
using BAL.DAOs.Interfaces;
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
                    Type = create.Type,
                    Description = create.Description,
                    CreateDateTime = DateTime.Now,
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
                _questionRepository.Delete(key);
                _questionRepository.Commit();
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
                List<GetQuestion> listQuestion = _mapper.Map<List<GetQuestion>>(_questionRepository.Get().ToList());
                Question question = _questionRepository.GetByID(key);

                if (question == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetQuestion result = listQuestion.FirstOrDefault(p => p.Id == question.Id);
                return _mapper.Map<GetQuestion>(result);
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
                List<GetQuestion> listQuestion = _mapper.Map<List<GetQuestion>>(_questionRepository.Get().ToList());
                return listQuestion;
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

                existedQuestion.Type = update.Type;
                existedQuestion.Description = update.Description;
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
