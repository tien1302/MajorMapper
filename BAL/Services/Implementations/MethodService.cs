using AutoMapper;
using BAL.Services.Interfaces;
using BAL.DTOs.Methods;
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
    public class MethodService : IMethodService
    {
        private MethodRepository _repo;
        private IMapper _mapper;

        public MethodService(IMethodRepository repo, IMapper mapper)
        {
            _repo = (MethodRepository)repo;
            _mapper = mapper;
        }

        public void Create(CreateMethod createMethod)
        {
            try
            {
                var checkName = _repo.Get(filter: m => m.Name.Equals(createMethod.Name)).FirstOrDefault();
                if (checkName != null)
                {
                    throw new Exception("Tên phương pháp bị trùng");
                }

                Method method = new Method()
                {
                    Name = createMethod.Name,
                    Description = createMethod.Description,
                    CreateDateTime = DateTime.Now,
                };

                _repo.Insert(method);
                _repo.Commit();
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
                Method existedMethod = _repo.GetByID(key);
                if (existedMethod == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                _repo.Delete(key);
                _repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetMethod> GetAll()
        {
            try
            {
                List<GetMethod> listMethod = _mapper.Map<List<GetMethod>>(_repo.Get().ToList());
                return listMethod;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public GetMethod Get(int key)
        {
            try
            {
                List<GetMethod> listMethod = _mapper.Map<List<GetMethod>>(_repo.Get().ToList());
                Method method = _repo.GetByID(key);

                if (method == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                GetMethod result = listMethod.FirstOrDefault(p => p.Id == method.Id);
                return _mapper.Map<GetMethod>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateMethod updateMethod)
        {
            try
            {
                Method existedMethod = _repo.GetByID(key);
                if (existedMethod == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                var checkName = _repo.Get(filter: m => m.Id != key && m.Name.Equals(updateMethod.Name)).FirstOrDefault();
                if (checkName != null)
                {
                    throw new Exception("Tên ngành nghề bị trùng");
                }

                existedMethod.Name = updateMethod.Name;
                existedMethod.Description = updateMethod.Description;
                _repo.Update(existedMethod);
                _repo.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
