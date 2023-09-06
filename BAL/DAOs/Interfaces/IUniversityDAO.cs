using BAL.DTOs.Universities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IUniversityDAO
    {
        public List<GetUniversity> GetAll();
        public GetUniversity Get(int key);
        public void Create(CreateUniversity createUni);
        public void Update(int key, UpdateUniversity updateUni);
        public void Delete(int key);
    }
}
