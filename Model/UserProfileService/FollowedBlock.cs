using System.Collections.Generic;

namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileService
{
    public class FollowedBlock
    {
        public List<UserProfile> followed { get; private set; }
        public bool moreFollowed { get; private set; }

        public FollowedBlock(List<UserProfile> followed, bool moreFollowed)
        {
            this.followed = followed;
            this.moreFollowed = moreFollowed;
        }
    }
}
