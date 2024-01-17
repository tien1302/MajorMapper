using AutoMapper;
using BAL.Services.Interfaces;
using BAL.DTOs.PersonalityTypes;
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
    public class PersonalityTypeService : IPersonalityTypeService
    {
        private PersonalityTypeRepository _personalityTypeRepository;
        private MethodRepository _methodRepository;
        private IMapper _mapper;

        public PersonalityTypeService(IPersonalityTypeRepository personalityTypeRepository, IMethodRepository methodRepository, IMapper mapper)
        {
            _personalityTypeRepository = (PersonalityTypeRepository)personalityTypeRepository;
            _methodRepository = (MethodRepository)methodRepository;
            _mapper = mapper;
        }

        public void Create(CreatePersonalityType createPersonalityType)
        {
            try
            {
                var checkName = _personalityTypeRepository.Get(filter: m => m.Name.Equals(createPersonalityType.Name)).FirstOrDefault();
                if (checkName != null)
                {
                    throw new Exception("Tên tính cách bị trùng");
                }

                PersonalityType personalityType = new PersonalityType()
                {
                    Name = createPersonalityType.Name,
                    MethodId = createPersonalityType.MethodId,
                    Description = createPersonalityType.Description,
                    CreateDateTime = DateTime.Now,
                    UpdateDateTime = DateTime.Now,
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
                GetPersonalityType result = _mapper.Map<GetPersonalityType>(_personalityTypeRepository.Get(filter: x => x.Id == key).FirstOrDefault());

                if (result == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                
                return _mapper.Map<GetPersonalityType>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Lấy danh sách personalityType theo methodId
        public List<GetPersonalityType> GetAllByMethodId(int methodId)
        {
            try
            {
                List<GetPersonalityType> listPersonalityType = _mapper.Map<List<GetPersonalityType>>(_personalityTypeRepository.Get(filter: p => p.MethodId == methodId).ToList());
                return listPersonalityType;
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

                var checkName = _personalityTypeRepository.Get(filter: m => m.Id != key && m.Name.Equals(updatePersonalityType.Name.Trim())).FirstOrDefault();
                if (checkName != null)
                {
                    throw new Exception("Tên tính cách bị trùng");
                }

                existedPersonalityType.Name = updatePersonalityType.Name;
                existedPersonalityType.Description = updatePersonalityType.Description;
                existedPersonalityType.MethodId = updatePersonalityType.MethodId;
                existedPersonalityType.UpdateDateTime = DateTime.Now;
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
