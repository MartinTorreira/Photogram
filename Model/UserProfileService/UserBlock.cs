using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Es.Udc.DotNet.PracticaMaD.Model.UserProfileService
{
    public class UserBlock
    {
        public List<UserProfile> Users { get; private set; }
        public bool moreUsers { get; private set; }



        public UserBlock(List<UserProfile> users, bool moreUsers)
        {
            this.Users = users;
            this.moreUsers = moreUsers;
        }
    }
}

