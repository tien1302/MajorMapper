using BAL.DTOs.Slots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.DAOs.Interfaces
{
    public interface ISlotDAO
    {
        public List<GetSlot> GetAll();
        public List<GetSlot> Get(int key);
        public List<GetSlot> CheckStatus();
        public List<GetSlot> GetAllSlotActive();
        public GetSlot GetBySlotId(int key);
        public void Create(bool allDay, int auto, CreateSlot createSlot);
        public void Update(int key);
        public void Delete(int key);
    }
}
