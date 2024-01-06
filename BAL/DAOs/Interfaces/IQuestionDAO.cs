using BAL.DTOs.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IQuestionDAO
    {
        public List<GetQuestion> GetAll();
        public List<GetQuestion> GetProcessing();
        public GetQuestion Get(int key);
        public void Create(CreateQuestion create);
        public void Update(int key, UpdateQuestion update);
        public void Delete(int key);
    }
}
