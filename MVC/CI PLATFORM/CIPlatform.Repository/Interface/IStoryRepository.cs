using CIPlatform.Entitites.ViewModel;
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

        List<CIPlatform.Entitites.Models.Mission> mission_of_user(long user_id);

        bool AddStory(long user_id, long id, long mission_id, string title, string mystory, List<string> media, string type);
        Mission GetFileredStories(int page_index, long user_id);

        Entitites.ViewModel.StoryViewModel GetStory(long user_id, long Storyid);

        bool Recommend(string[] email, long Storyid, long user_id);
        void Views(long user_id, long Storyid);

    }
}
