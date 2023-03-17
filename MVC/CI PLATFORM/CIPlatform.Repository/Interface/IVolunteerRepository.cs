using CIPlatform.Entitites.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IVolunteerRepository : IRepository<CIPlatform.Entitites.Models.Mission>
    {
        void save();

        List<CIPlatform.Entitites.ViewModel.VolunteerViewModel> GetAllMission(int Id);
        VolunteerViewModel Missiondetails(int Id);
    }
}
