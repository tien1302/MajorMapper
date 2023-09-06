using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Majors;
using BAL.DTOs.Universities;
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
    public class MajorDAO : IMajorDAO
    {
        private MajorRepository _majorRepository;
        private IMapper _mapper;

        public MajorDAO(IMajorRepository majorRepository, IMapper mapper)
        {
            _majorRepository = (MajorRepository)majorRepository;
            _mapper = mapper;
        }

        public void Create(CreateMajor createMajor)
        {
            try
            {
                Major major = new Major()
                {
                    Name = createMajor.Name,
                    Description = createMajor.Description,
                    CreatedDateTime = DateTime.Now,
                };
                _majorRepository.Insert(major);
                _majorRepository.Commit();
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
                Major existedMajor = _majorRepository.GetByID(key);
                if (existedMajor == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
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
                List<GetMajor> listMajor = _mapper.Map<List<GetMajor>>(_majorRepository.Get().ToList());
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
                List<GetMajor> listMajor = _mapper.Map<List<GetMajor>>(_majorRepository.Get().ToList());
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
                Major existedMajor = _majorRepository.GetByID(key);
                if (existedMajor == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedMajor.Name = updateMajor.Name;
                existedMajor.Description = updateMajor.Description;
                existedMajor.UpdatedDateTime = DateTime.Now;
                _majorRepository.Update(existedMajor);
                _majorRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
