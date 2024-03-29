﻿using AutoMapper;
using BAL.Services.Interfaces;
using BAL.DTOs.Majors;
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
    public class MajorService : IMajorService
    {
        private MajorRepository _majorRepository;
        private PersonalityTypeRepository _personalityTypeRepository;
        private IMapper _mapper;

        public MajorService(IMajorRepository majorRepository, IPersonalityTypeRepository personalityTypeRepository, IMapper mapper)
        {
            _majorRepository = (MajorRepository)majorRepository;
            _personalityTypeRepository = (PersonalityTypeRepository)personalityTypeRepository;
            _mapper = mapper;
        }

        public void Create(CreateMajor createMajor)
        {
            try
            {
                List<PersonalityType> listPersonalityType = new List<PersonalityType>();
                var listPersonalityTypeId = createMajor.PersonalityTypeId;
                foreach (int id in listPersonalityTypeId)
                {
                    var checkPersonalityTypeId = _personalityTypeRepository.GetByID(id);
                    if (checkPersonalityTypeId == null)
                    {
                        throw new Exception("PersonalityType Id does not exist in the system.");
                    }
                    listPersonalityType.Add(checkPersonalityTypeId);
                }

                var checkName = _majorRepository.Get(filter: m => m.Name.Equals(createMajor.Name.Trim())).FirstOrDefault();
                if (checkName != null)
                {
                    throw new Exception("Tên ngành nghề bị trùng");
                }

                Major major = new Major()
                {
                    Name = createMajor.Name,
                    Description = createMajor.Description,
                    CreateDateTime = DateTime.Now,
                    UpdateDateTime = DateTime.Now,
                };
                _majorRepository.Insert(major);
                _majorRepository.Commit();

                foreach (var item in listPersonalityType)
                {
                    item.Majors.Add(major);
                    _personalityTypeRepository.Update(item);
                    _personalityTypeRepository.Commit();
                }
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
                Major existedMajor = _majorRepository.Get(filter: u => u.Id == key, includeProperties: "PersonalityTypes").FirstOrDefault();
                if (existedMajor == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedMajor.PersonalityTypes.Clear();
                _majorRepository.Delete(key);
                _majorRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetMajor Get(int key)
        {
            try
            {
                List<GetMajor> listMajor = _mapper.Map<List<GetMajor>>(_majorRepository.Get(includeProperties: "PersonalityTypes").ToList());
                Major major = _majorRepository.GetByID(key);

                if (major == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetMajor result = listMajor.FirstOrDefault(p => p.Id == major.Id);
                return _mapper.Map<GetMajor>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetMajor> GetAll()
        {
            try
            {
                List<GetMajor> listMajor = _mapper.Map<List<GetMajor>>(_majorRepository.Get(includeProperties: "PersonalityTypes").ToList());
                return listMajor;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateMajor updateMajor)
        {
            try
            {
                List<PersonalityType> listPersonalityType = new List<PersonalityType>();
                var listPersonalityTypeId = updateMajor.PersonalityTypeId;
                foreach (int id in listPersonalityTypeId)
                {
                    var checkPersonalityTypeId = _personalityTypeRepository.GetByID(id);
                    if (checkPersonalityTypeId == null)
                    {
                        throw new Exception("PersonalityType Id does not exist in the system.");
                    }
                    listPersonalityType.Add(checkPersonalityTypeId);
                }

                Major existedMajor = _majorRepository.Get(filter: u => u.Id == key, includeProperties: "PersonalityTypes").FirstOrDefault();
                if (existedMajor == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                var checkName = _majorRepository.Get(filter: m => m.Id != key && m.Name.Equals(updateMajor.Name)).FirstOrDefault();
                if (checkName != null)
                {
                    throw new Exception("Tên phương pháp bị trùng");
                }

                existedMajor.Name = updateMajor.Name;
                existedMajor.Description = updateMajor.Description;
                existedMajor.UpdateDateTime = DateTime.Now;
                existedMajor.PersonalityTypes.Clear();
                _majorRepository.Update(existedMajor);
                _majorRepository.Commit();

                foreach (var item in listPersonalityType)
                {
                    item.Majors.Add(existedMajor);
                    _personalityTypeRepository.Update(item);
                    _personalityTypeRepository.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
