using BAL.DTOs.ReviewTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IReviewTestDAO
    {
        public List<GetReviewTest> GetAll();
        public GetReviewTest Get(int key);
        public void Create(CreateReviewTest create);
        public void Delete(int key);
    }
}
