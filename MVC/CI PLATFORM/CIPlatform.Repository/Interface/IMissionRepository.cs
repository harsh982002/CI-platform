﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IMissionRepository : IRepository<CIPlatform.Entitites.Models.Mission>
    {
        void Save();
        Entitites.ViewModel.Mission GetAllMission();
        Entitites.ViewModel.Mission GetFilteredMissions(List<string> countries, List<string> cities, List<string> themes, List<string> skills, string sort_by, int page_index, long user_id);
        Entitites.ViewModel.Mission GetSearchMissions(string key, int page_index);
        CIPlatform.Entitites.ViewModel.VolunteerViewModel Mission(long id, long user_id);
        void AddComment(string comment, long MissonId, long userId);
        bool ApplyMission(long user_id, long mission_id);
        bool add_to_favourite(long user_id, long mission_id);

        bool AddFavouriteMission(long MissionId, long UserId);
        bool sendMail(string[] email, long mission_id, long user_id);
      
    }
}
