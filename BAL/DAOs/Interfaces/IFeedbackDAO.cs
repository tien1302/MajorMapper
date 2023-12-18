using BAL.DTOs.Feedbacks;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IFeedbackDAO
    {
        public List<GetFeedback> GetAll();
        public GetFeedback Get(int key);
        public void Create(CreateFeedback create);
        public void Update(int key, UpdateFeedback update);
        public void Delete(int key);
        public List<GetFeedback> GetFeedbackAccount(int key);
    }
}
