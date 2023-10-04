using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.PersonalityTypes;
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
    public class PersonalityTypeDAO : IPersonalityTypeDAO
    {
        private PersonalityTypeRepository _personalityTypeRepository;
        private IMapper _mapper;

        public PersonalityTypeDAO(IPersonalityTypeRepository personalityTypeRepository, IMapper mapper)
        {
            _personalityTypeRepository = (PersonalityTypeRepository)personalityTypeRepository;
            _mapper = mapper;
        }

        public void Create(CreatePersonalityType createPersonalityType)
        {
            try
            {
                PersonalityType personalityType = new PersonalityType()
                {
                    Name = createPersonalityType.Name,
                    CreatedDateTime = DateTime.Now,
                };
                _personalityTypeRepository.Insert(personalityType);
                _personalityTypeRepository.Commit();
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
                PersonalityType existedPersonalityType = _personalityTypeRepository.GetByID(key);
                if (existedPersonalityType == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                _personalityTypeRepository.Delete(key);
                _personalityTypeRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetPersonalityType Get(int key)
        {
            try
            {
                List<GetPersonalityType> listPersonalityType = _mapper.Map<List<GetPersonalityType>>(_personalityTypeRepository.Get().ToList());
                PersonalityType personalityType = _personalityTypeRepository.GetByID(key);

                if (personalityType == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetPersonalityType result = listPersonalityType.FirstOrDefault(p => p.Id == personalityType.Id);
                return _mapper.Map<GetPersonalityType>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetPersonalityType> GetAll()
        {
            try
            {
                List<GetPersonalityType> listPersonalityType = _mapper.Map<List<GetPersonalityType>>(_personalityTypeRepository.Get().ToList());
                return listPersonalityType;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdatePersonalityType updatePersonalityType)
        {
            try
            {
                PersonalityType existedPersonalityType = _personalityTypeRepository.GetByID(key);
                if (existedPersonalityType == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedPersonalityType.Name = updatePersonalityType.Name;
                existedPersonalityType.UpdatedDateTime = DateTime.Now;
                _personalityTypeRepository.Update(existedPersonalityType);
                _personalityTypeRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
