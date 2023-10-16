using BAL.DTOs.Scores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface IScoreDAO
    {
        public List<GetScore> GetAll();
        public GetScore Get(int key);
        public void Create(CreateScore create);
        public void Update(int key, UpdateScore update);
        public void Delete(int key);
    }
}
