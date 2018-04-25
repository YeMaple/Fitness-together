using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;
using System.Linq;

namespace DAL
{
    public class FollowingsAccess
    {
        private readonly cse136Context _context;

        public FollowingsAccess(cse136Context context)
        {
            _context = context;
        }

        public IEnumerable<Followings> GetFollowings(int follower_id)
        {
            var followingQuery = _context.Followings
                                 .Where(f => f.Follower.Equals(follower_id))
                                 .ToList();
            return followingQuery;
        }
    }
}
