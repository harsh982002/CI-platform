using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IProfileRepository 
    {
        bool Change_Password(string oldpassword, string newpassword, long user_id);
        bool profile_update(CIPlatform.Entitites.ViewModel.ProfileViewModel userdetail, long user_id);
        CIPlatform.Entitites.ViewModel.ProfileViewModel Get_details(int country, long user_Id);
        bool contact_us(long user_id, string name, string email, string subject , string message);
    }
}
