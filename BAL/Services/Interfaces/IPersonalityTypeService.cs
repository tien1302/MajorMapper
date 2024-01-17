using BAL.DTOs.PersonalityTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IPersonalityTypeService
    {
        public List<GetPersonalityType> GetAll();
        //Lấy danh sách personalityType theo methodId
        public List<GetPersonalityType> GetAllByMethodId(int methodId);
        public GetPersonalityType Get(int key);
        public void Create(CreatePersonalityType createPersonalityType);
        public void Update(int key, UpdatePersonalityType updatePersonalityType);
        public void Delete(int key);
    }
}
