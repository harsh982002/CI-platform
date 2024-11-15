﻿using CIPlatform.Entitites.ViewModel;
using System;
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
        public IProfileRepository profileRepository { get; set; }
        public IStoryRepository storyRepository { get; set; }
        public ICMSRepository cmsRepository { get; set; }
    }
}
