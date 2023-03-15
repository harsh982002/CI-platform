


using CIPlatform.Entities.ViewModel;
using CIPlatform.Entitites.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IAccountRepository
    {


        public string Message();

        public int GetUserID(string Email);


        public User LoginViewModel(LoginViewModel model);

        public User RegistrationViewModel(RegistrationViewModel model);
        public Boolean IsValidUserEmail(RegistrationViewModel model);

        public PasswordReset ResetPasswordViewModel(ResetPasswordViewModel model, string token);
        public User ForgotPasswordViewModel(ForgotPasswordViewModel model);



    }
}