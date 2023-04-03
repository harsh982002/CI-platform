using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IProfileRepository 
    {
        CIPlatform.Entitites.ViewModel.ProfileViewModel Get_details(int country);
    }
}
