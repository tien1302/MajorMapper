using AutoMapper;
using BAL.DAOs.Interfaces;
using BAL.DTOs.Slots;
using DAL.Models;
using DAL.Repositories.Implementations;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
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
                if(createSlot.AllDay)
                {
                    CreateAutoSlotsAllDay(createSlot);
                }
                else 
                {
                    CreateAutoOneSlot(createSlot);
                }
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
                List<GetSlot> listSlot = _mapper.Map<List<GetSlot>>(_slotRepository.Get(filter: s => s.ConsultantId == key).ToList());
                return listSlot;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Hàm lấy list slot trống theo consultantId cho mobile
        public List<GetSlot> GetAllSlotActive()
        {
            try
            {
                List<GetSlot> listSlot = _mapper.Map<List<GetSlot>>(
                                         _slotRepository.Get(filter: s => s.Status == "Available",
                                                             orderBy: q => q.OrderBy(s => s.StartDateTime)
                                                                            .ThenBy(s => s.CreateDateTime)).ToList());
                return listSlot;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Tự động cập nhật trạng thái "Hoàn thành" cho list slot
        public List<GetSlot> CheckStatus()
        {
            try
            {
                string formattedTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
                DateTime check = DateTime.Parse(formattedTime);
                List<Slot> list = _slotRepository.Get(filter: s => s.EndDateTime <= check).ToList();
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

        //Tạo tự động nhiều slot cho cả ngày
        public void CreateAutoSlotsAllDay(CreateSlot createSlot)
        {
            try
            {
                if (DateTime.Compare(createSlot.Date, DateTime.Now.Date) < 0)
                {
                    throw new Exception("Chỉ được chọn ngày hôm nay trở đi");
                }
                DateTime startDateTime;
                if (createSlot.Auto == 1)
                {
                    DateTime endDaily = createSlot.Date.AddDays(6);
                    for (DateTime date = createSlot.Date; date <= endDaily; date = date.AddDays(1))
                    {
                        for (int hour = 7; hour <= 20; hour++)
                        {
                            startDateTime = new DateTime(date.Date.Year, date.Date.Month, date.Date.Day, hour, 0, 0);
                            if (DateTime.Compare(startDateTime, DateTime.Now) > 0)
                            {
                                Slot slot = new Slot()
                                {
                                    ConsultantId = createSlot.ConsultantId,
                                    StartDateTime = startDateTime,
                                    EndDateTime = startDateTime.AddHours(1),
                                    CreateDateTime = DateTime.Now,
                                    Status = "Available",
                                };
                                if (_slotRepository.Get().Any(s => s.StartDateTime == startDateTime && s.ConsultantId == createSlot.ConsultantId))
                                {
                                    continue; // Skip if slot already exists
                                }

                                _slotRepository.Insert(slot);
                            }
                        }
                    }
                    _slotRepository.Commit();
                }
                else if(createSlot.Auto == 2)
                {
                    for (int week = 0; week <= 3; week++)
                    {
                        for (int hour = 7; hour <= 20; hour++)
                        {
                            startDateTime = new DateTime(createSlot.Date.Year, createSlot.Date.Month, createSlot.Date.Day, hour, 0, 0);
                            DateTime dateTime = startDateTime.AddDays(week * 7);
                            if (DateTime.Compare(startDateTime, DateTime.Now) > 0)
                            {
                                Slot slot = new Slot()
                                {
                                    ConsultantId = createSlot.ConsultantId,
                                    StartDateTime = dateTime,
                                    EndDateTime = dateTime.AddHours(1),
                                    CreateDateTime = DateTime.Now,
                                    Status = "Available",
                                };
                                if (_slotRepository.Get().Any(s => s.StartDateTime == dateTime && s.ConsultantId == createSlot.ConsultantId))
                                {
                                    continue; // Skip if slot already exists
                                }

                                _slotRepository.Insert(slot);
                            }
                        }
                    }
                    _slotRepository.Commit();
                }
                else if (createSlot.Auto == 3)
                {
                    for (int hour = 7; hour <= 20; hour++)
                    {
                        startDateTime = new DateTime(createSlot.Date.Year, createSlot.Date.Month, createSlot.Date.Day, hour, 0, 0);
                        if (DateTime.Compare(startDateTime, DateTime.Now) > 0)
                        {
                            Slot slot = new Slot()
                            {
                                ConsultantId = createSlot.ConsultantId,
                                StartDateTime = startDateTime,
                                EndDateTime = startDateTime.AddHours(1),
                                CreateDateTime = DateTime.Now,
                                Status = "Available",
                            };
                            if (_slotRepository.Get().Any(s => s.StartDateTime == startDateTime && s.ConsultantId == createSlot.ConsultantId))
                            {
                                continue; // Skip if slot already exists
                            }

                            _slotRepository.Insert(slot);
                        }
                    }
                    _slotRepository.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Tạo tự động 1 slot
        public void CreateAutoOneSlot(CreateSlot createSlot)
        {
            try
            {
                DateTime startDateTime = new DateTime(createSlot.Date.Year, createSlot.Date.Month, createSlot.Date.Day, createSlot.StartDateTime.Hour, 0, 0);
                if (DateTime.Compare(startDateTime, DateTime.Now) < 0)
                {
                    throw new Exception("Chỉ được chọn ngày hôm nay trở đi");
                }
                if (createSlot.Auto == 1)
                {
                    DateTime endDaily = createSlot.Date.AddDays(6);
                    for (DateTime date = createSlot.Date; date <= endDaily; date = date.AddDays(1))
                    {
                        DateTime dateTime = new DateTime(date.Date.Year, date.Date.Month, date.Date.Day, createSlot.StartDateTime.Hour, 0, 0);
           
                        Slot slot = new Slot()
                        {
                            ConsultantId = createSlot.ConsultantId,
                            StartDateTime = dateTime,
                            EndDateTime = dateTime.AddHours(1),
                            CreateDateTime = DateTime.Now,
                            Status = "Available",
                        };
                        if (_slotRepository.Get().Any(s => s.StartDateTime == startDateTime && s.ConsultantId == createSlot.ConsultantId))
                        {
                            continue; // Skip if slot already exists
                        }

                        _slotRepository.Insert(slot);
                    }
                    _slotRepository.Commit();
                }
                else if (createSlot.Auto == 2)
                {
                    for (int week = 0; week <= 3; week++)
                    {
                        startDateTime = new DateTime(createSlot.Date.Year, createSlot.Date.Month, createSlot.Date.Day, createSlot.StartDateTime.Hour, 0, 0);
                        DateTime dateTime = startDateTime.AddDays(week*7);
                        Slot slot = new Slot()
                        {
                            ConsultantId = createSlot.ConsultantId,
                            StartDateTime = dateTime,
                            EndDateTime = dateTime.AddHours(1),
                            CreateDateTime = DateTime.Now,
                            Status = "Available",
                        };
                        if (_slotRepository.Get().Any(s => s.StartDateTime == dateTime && s.ConsultantId == createSlot.ConsultantId))
                        {
                            continue; // Skip if slot already exists
                        }

                        _slotRepository.Insert(slot);
                    }
                    _slotRepository.Commit();
                }
                else if (createSlot.Auto == 3)
                {
                    List<GetSlot> listSlot = _mapper.Map<List<GetSlot>>(_slotRepository.Get(filter: s => s.StartDateTime == startDateTime && s.ConsultantId == createSlot.ConsultantId).ToList());
                    if (listSlot.Count != 0)
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
