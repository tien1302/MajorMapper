using BAL.DTOs.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface IMethodService
    {
        public List<GetMethod> GetAll();
        public GetMethod Get(int key);
        public void Create(CreateMethod createMethod);
        public void Update(int key, UpdateMethod updateMethod);
        public void Delete(int key);
        
    }
}
