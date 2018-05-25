using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface FollowingsAccessInterface
    {
        IEnumerable<Followings> GetFollowings(int follower_id);
    }
}
