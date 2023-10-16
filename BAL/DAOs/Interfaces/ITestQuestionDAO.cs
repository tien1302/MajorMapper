using BAL.DTOs.TestQuestions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface ITestQuestionDAO
    {
        public List<GetTestQuestion> GetAll();
        public GetTestQuestion Get(int key);
        public void Create(CreateTestQuestion create);
        public void Update(int key, UpdateTestQuestion update);
        public void Delete(int key);
    }
}
