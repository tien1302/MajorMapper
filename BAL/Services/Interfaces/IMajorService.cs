using BAL.DTOs.Majors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMajorService
    {
        public List<GetMajor> GetAll();
        public GetMajor Get(int key);
        public void Create(CreateMajor createMajor);
        public void Update(int key, UpdateMajor updateMajor);
        public void Delete(int key);
    }
}
