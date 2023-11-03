using AutoMapper;
using BAL.DAOs.Interfaces;
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
                Slot slot = new Slot()
                {
                    ConsultantId = createSlot.ConsultantId,
                    StartDateTime = new DateTime(createSlot.Date.Year, createSlot.Date.Month, createSlot.Date.Day,  createSlot.StartDateTime.Hour, createSlot.StartDateTime.Minute, createSlot.StartDateTime.Second),
                    EndDateTime = new DateTime(createSlot.Date.Year, createSlot.Date.Month, createSlot.Date.Day, createSlot.StartDateTime.AddHours(1).Hour, createSlot.StartDateTime.Minute, createSlot.StartDateTime.Second),
                    CreateDateTime = DateTime.Now,
                    Status = "Not Booked",
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

        public List<GetSlot> GetAll()
        {
            try
            {
                List<GetSlot> listSlot = _mapper.Map<List<GetSlot>>(_slotRepository.Get().ToList());
                return listSlot;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(int key, UpdateSlot updateSlot)
        {
            try
            {
                Slot existedSlot = _slotRepository.GetByID(key);
                if (existedSlot == null)
                {
                    throw new Exception("Id does not exist in the system.");
                }

                existedSlot.StartDateTime = updateSlot.StartDateTime;
                existedSlot.EndDateTime = updateSlot.EndDateTime;
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
