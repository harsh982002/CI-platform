using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IMissionRepository : IRepository<CIPlatform.Entitites.Models.Mission>
    {
        void Save();


        List<CIPlatform.Entitites.ViewModel.Mission> GetAllMission();
        List<CIPlatform.Entitites.ViewModel.Mission> GetFilteredMissions(List<string> Countries, List<string> Cities, List<string> Themes, List<string> Skills, string sort_by, int page_index);
        List<CIPlatform.Entitites.ViewModel.Mission> GetSearchMissions(string key, int page_index);
        CIPlatform.Entitites.Models.Mission Mission(long id);
    }
}
