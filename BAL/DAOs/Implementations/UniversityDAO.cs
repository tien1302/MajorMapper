using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Universities;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Implementations
{
    public class UniversityDAO : IUniversityDAO
    {
        private UniversityRepository _uniRepository;
        private MajorRepository _majorRepository;
        private IMapper _mapper;

        public UniversityDAO(IUniversityRepository uniRepository, IMajorRepository majorRepository, IMapper mapper)
        {
            _uniRepository = (UniversityRepository)uniRepository;
            _majorRepository = (MajorRepository)majorRepository;
            _mapper = mapper;
        }

        public void Create(CreateUniversity createUni)
        {
            try
            {
                List<Major> listMajor = new List<Major>();
                var listMajorId = createUni.MajorId;
                foreach (int id in listMajorId)
                {
                    var checkMajorId = _majorRepository.GetByID(id);
                    if (checkMajorId == null)
                    {
                        throw new Exception("Major Id does not exist in the system.");
                    }
                    listMajor.Add(checkMajorId);
                }

                University university = new University()
                {
                    Name = createUni.Name,
                    Address = createUni.Address,
                    Phone = createUni.Phone,
                    Email = createUni.Email,
                    Website = createUni.Website,
                    Icon = createUni.Icon,
                    CreatedDateTime = DateTime.Now
                };

                _uniRepository.Insert(university);
                _uniRepository.Commit();

                foreach (var item in listMajor) 
                {
                    item.Universities.Add(university);
                    _majorRepository.Update(item);
                    _majorRepository.Commit();
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
                University existedUni = _uniRepository.Get(includeProperties: "Majors").FirstOrDefault(u => u.Id == key);
                if (existedUni == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                existedUni.Majors.Clear();
                _uniRepository.Delete(key);
                _uniRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetUniversity Get(int key)
        {
            try
            {
                List<GetUniversity> listUni = _mapper.Map<List<GetUniversity>>(_uniRepository.Get(includeProperties: "Majors").ToList());
                University university = _uniRepository.GetByID(key);

                if (university == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetUniversity result = listUni.FirstOrDefault(p => p.Id == university.Id);
                return _mapper.Map<GetUniversity>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetUniversity> GetAll()
        {
            try
            {
                List<GetUniversity> listUni = _mapper.Map<List<GetUniversity>>(_uniRepository.Get().ToList());
                return listUni;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateUniversity updateUni)
        {
            try
            {
                List<Major> listMajor = new List<Major>();
                var listMajorId = updateUni.MajorId;
                foreach (int id in listMajorId)
                {
                    var checkMajorId = _majorRepository.GetByID(id);
                    if (checkMajorId == null)
                    {
                        throw new Exception("Major Id does not exist in the system.");
                    }
                    listMajor.Add(checkMajorId);
                }

                University existedUni = _uniRepository.Get(includeProperties: "Majors").FirstOrDefault(u => u.Id == key);
                if (existedUni == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                existedUni.Name = updateUni.Name;
                existedUni.Address = updateUni.Address;
                existedUni.Phone = updateUni.Phone;
                existedUni.Email = updateUni.Email;
                existedUni.Website = updateUni.Website;
                existedUni.Icon = updateUni.Icon;
                existedUni.UpdatedDateTime = DateTime.Now;
                existedUni.Majors.Clear();
                _uniRepository.Update(existedUni);
                _uniRepository.Commit();

                foreach (var item in listMajor)
                {
                    item.Universities.Add(existedUni);
                    _majorRepository.Update(item);
                    _majorRepository.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
