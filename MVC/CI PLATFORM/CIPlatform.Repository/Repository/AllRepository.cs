using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.ViewModel;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class AllRepository : IAllRepository
    {
        private readonly CiplatformContext _db;

        public AllRepository(CiplatformContext db)
        {
            _db= db;
            missionRepository=new MissionRepository(_db);
            volunteerRepository = new VolunteerRepository(_db); 
        }
        public IMissionRepository missionRepository { get; set; }
        public IVolunteerRepository volunteerRepository { get; set ; }

        public void save()
        {
            _db.SaveChanges();
        }
    }
}
