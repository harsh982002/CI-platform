using CIPlatform.Entitites.Data;
using CIPlatform.Entitites.Models;
using CIPlatform.Entitites.ViewDataModel;
using CIPlatform.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CiplatformContext _db;

        public AccountRepository(CiplatformContext db)
        {
            _db = db;
        }
        public User LoginViewModel(LoginViewModel model)
        {
            return _db.Users.FirstOrDefault(x => x.Email == model.Email || x.Password == model.Password);
        }

        public User RegistrationViewModel(RegistrationViewModel model)
        {
            User user = new User();
            user.FirstName = model.FirstName;   
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;   
            user.Email = model.Email;
            user.Password = model.Password;
            var entry = _db.Add(user);
            _db.SaveChanges();
            return entry.Entity;
            
        }
    }
}
