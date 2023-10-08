﻿using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.PersonalityTypes;
using BAL.DTOs.Slots;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                    StartDateTime = createSlot.StartDateTime,
                    EndDateTime = createSlot.EndDateTime,
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

        public GetSlot Get(int key)
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