using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileService
{
    public class FollowersBlock
    {
        public List<UserProfile> followers { get; private set; }
        public bool moreFollowers { get; private set; }

        public FollowersBlock(List<UserProfile> followers, bool moreFollowers)
        {
            this.followers = followers;
            this.moreFollowers = moreFollowers;
        }
    }
}
