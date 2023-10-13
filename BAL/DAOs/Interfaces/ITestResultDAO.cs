using BAL.DTOs.TestResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface ITestResultDAO
    {
        public List<GetTestResult> GetAll();
        public GetTestResult Get(int key);
        public void Create(CreateTestResult create);
        public void Delete(int key);
    }
}
