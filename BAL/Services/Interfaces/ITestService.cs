using BAL.DTOs.Scores;
using BAL.DTOs.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface ITestService
    {
        public List<GetTest> GetAll();
        public GetTest Get(int key);
        public void Create(CreateTest create);
        public void Update(int key);
        public void Delete(int key);
        public List<GetTest> GetTestbyAccountId(int key);
    }
}
