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
        // Hàm lấy list slot theo consultantId
        public List<GetSlot> Get(int key);
        //Tự động cập nhật trạng thái "Hoàn thành" cho list slot
        public List<GetSlot> CheckStatus();
        // Hàm lấy list slot trống theo consultantId cho mobile
        public List<GetSlot> GetAllSlotActive();
        // Hàm lấy slot theo SlotId
        public GetSlot GetBySlotId(int key);
        public void Create(CreateSlot createSlot);
        public void Delete(int key);
        public void Update(int key);
    }
}
