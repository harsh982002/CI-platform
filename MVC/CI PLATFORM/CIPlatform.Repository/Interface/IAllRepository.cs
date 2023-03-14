﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IAllRepository
    {
        void save();

        public IMissionRepository missionRepository { get; set; }
    }
}