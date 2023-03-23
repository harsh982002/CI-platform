using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIPlatform.Repository.Interface
{
    public interface IStoryRepository
    {

        CIPlatform.Entitites.ViewModel.Mission GetStories(long user_id);
    }
}
