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
        User Login(User obj);
        User Register(User obj);

        User Forgot(User model);
        PasswordReset Reset(User model, string token);
    }
}
