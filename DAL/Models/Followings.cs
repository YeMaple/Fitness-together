using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public partial class Followings
    {
        public int Id { get; set; }
        public int Follower { get; set; }
        public int Following { get; set; }

        public Persons FollowerNavigation { get; set; }
        public Persons FollowingNavigation { get; set; }
    }
}
