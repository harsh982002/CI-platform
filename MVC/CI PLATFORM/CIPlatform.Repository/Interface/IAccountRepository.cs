using CIPlatform.Entitites.Models;
using CIPlatform.Entitites.ViewDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IAccountRepository
    {
        public User LoginViewModel(LoginViewModel model);

        public User RegistrationViewModel(RegistrationViewModel model);
    }
}
