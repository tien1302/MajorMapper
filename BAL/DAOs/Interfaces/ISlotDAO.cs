﻿using BAL.DTOs.Slots;
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
        public GetSlot Get(int key);
        public void Create(CreateSlot createSlot);
        public void Update(int key, UpdateSlot updateSlot);
        public void Delete(int key);
    }
}