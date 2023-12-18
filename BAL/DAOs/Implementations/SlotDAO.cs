using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Majors;
using BAL.DTOs.Slots;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BAL.DAOs.Implementations
{
    public class SlotDAO : ISlotDAO
    {
        private SlotRepository _slotRepository;
        private IMapper _mapper;

        public SlotDAO(ISlotRepository slotRepository, IMapper mapper)
        {
            _slotRepository = (SlotRepository)slotRepository;
            _mapper = mapper;
        }

        public void Create(CreateSlot createSlot)
        {
            try
            {
                DateTime startDateTime = new DateTime(createSlot.Date.Year, createSlot.Date.Month, createSlot.Date.Day, createSlot.StartDateTime.Hour, createSlot.StartDateTime.Minute, createSlot.StartDateTime.Second);

                List<GetSlot> listSlot = _mapper.Map<List<GetSlot>>(_slotRepository.Get().ToList());
                if (listSlot.Any(slot => slot.StartDateTime == startDateTime && slot.ConsultantId == createSlot.ConsultantId))
                {
                    throw new Exception("Slot đã tồn tại.");
                }
                Slot slot = new Slot()
                {
                    ConsultantId = createSlot.ConsultantId,
                    StartDateTime = startDateTime,
                    EndDateTime = startDateTime.AddHours(1),
                    CreateDateTime = DateTime.Now,
                    Status = "Available",
                };
                _slotRepository.Insert(slot);
                _slotRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int key)
        {
            try
            {
                Slot existedSlot = _slotRepository.GetByID(key);
                if (existedSlot == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                _slotRepository.Delete(key);
                _slotRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Hàm lấy list slot theo consultantId
        public List<GetSlot> Get(int key)
        {
            try
            {
                List<GetSlot> listSlot = _mapper.Map<List<GetSlot>>(_slotRepository.Get().ToList());
            
                List<GetSlot> result = listSlot.Where(p => p.ConsultantId == key).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Hàm lấy list slot trống theo consultantId cho mobile
        public List<GetSlot> GetSlotActive(int key)
        {
            try
            {
                List<GetSlot> listSlot = _mapper.Map<List<GetSlot>>(_slotRepository.Get(filter: p => p.ConsultantId == key && p.Status == "Not Booked").ToList());
                return listSlot;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetSlot> CheckStatus(int key)
        {
            try
            {
                string formattedTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                DateTime check = DateTime.Parse(formattedTime);
                List<Slot> list = _slotRepository.Get().Where(s => s.ConsultantId == key && s.EndDateTime <= check ).ToList();
                if (list.Count > 0)
                {
                    foreach (var item in list)
                    {
                        if(item.Status != "Finish")
                        {
                            item.Status = "Finish";
                            _slotRepository.Update(item);
                            _slotRepository.Commit();
                        }
                    }
                }
                return _mapper.Map<List<GetSlot>>(list);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Hàm lấy slot theo SlotId
        public GetSlot GetBySlotId(int key)
        {
            try
            {
                List<GetSlot> listSlot = _mapper.Map<List<GetSlot>>(_slotRepository.Get().ToList());

                Slot slot = _slotRepository.GetByID(key);
                if (slot == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }
                GetSlot result = listSlot.FirstOrDefault(p => p.Id == slot.Id);
                return _mapper.Map<GetSlot>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<GetSlot> GetAll()
        {
            try
            {
                List<GetSlot> listSlot = _mapper.Map<List<GetSlot>>(_slotRepository.Get(filter: p => p.Consultant.Status == true).ToList());
                return listSlot;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key)
        {
            try
            {
                Slot existedSlot = _slotRepository.GetByID(key);
                if (existedSlot == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedSlot.Status = "Booking";
                _slotRepository.Update(existedSlot);
                _slotRepository.Commit();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
