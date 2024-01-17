using BAL.DTOs.TestResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Interfaces
{
    public interface ITestResultService
    {
        public List<GetTestResult> GetAll();
        public GetTestResult Get(int key);
        //Lấy test result theo methodName và testId
        public GetTestResult GetByMethodAndTest(string methodName, int testId);
        public void Create(CreateTestResult create);
        public void Delete(int key);
    }
}
