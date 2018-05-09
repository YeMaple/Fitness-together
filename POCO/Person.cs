using System.Collections.Generic;

namespace POCO
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Profile { get; set; }
        public string Sex { get; set; }
        public byte[] Image { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<int> MyDietPlansId { get; set; }
        public List<int> MyWorkoutsId { get; set; }
        public List<int> MyFollowingsId { get; set; }
        public List<int> MyPinnedWorkoutsId { get; set; }
        public List<int> MyPinnedDietPlansId { get; set; }
    }
}
