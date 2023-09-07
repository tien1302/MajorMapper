using BAL.DTOs.PersonalityTypes;
using BAL.DTOs.Universities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IPersonalityTypeDAO
    {
        public List<GetPersonalityType> GetAll();
        public GetPersonalityType Get(int key);
        public void Create(CreatePersonalityType createPersonalityType);
        public void Update(int key, UpdatePersonalityType updatePersonalityType);
        public void Delete(int key);
    }
}
