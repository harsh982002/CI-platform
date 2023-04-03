using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using CIPlatform.Entitites.ViewModel;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly CiplatformContext _db;

        public ProfileRepository(CiplatformContext db)
        {
            _db = db;
        }

        public ProfileViewModel Get_details(int country)
        {
          if(country == 0)
            {
                List<Country> countries = _db.Countries.ToList();
                List<Skill> skills = _db.Skills.ToList();
                List<City> cities = _db.Cities.ToList();
                return new ProfileViewModel { countries = countries, cities = cities, skill = skills };
            }
            else
            {
                List<City> cities = _db.Cities.Where(x=> x.CountryId == country).ToList();
                return new ProfileViewModel { cities= cities };

            }
        }
    }
}
