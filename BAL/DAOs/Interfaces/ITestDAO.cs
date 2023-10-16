using BAL.DTOs.Scores;
using BAL.DTOs.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface ITestDAO
    {
        public List<GetTest> GetAll();
        public GetTest Get(int key);
        public void Create(CreateTest create);
        public void Update(int key, UpdateTest update);
        public void Delete(int key);
    }
}
