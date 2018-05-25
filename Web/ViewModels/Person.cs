using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class Person
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Name cannot be blank"), MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email address cannot be blank"), MaxLength(50)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Passsword")]
        [Required(ErrorMessage = "Password cannot be blank"), MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Sex")]
        [MaxLength(1)]
        public string Sex { get; set; }

        [Display(Name = "Age")]
        [Range(1, 120, ErrorMessage = "Please put a correct age")]
        public int Age { get; set; }

        [Display(Name = "Profile")]
        [MaxLength(255)]
        public string Profile { get; set; }

        public List<DietPlan> MyDietPlans { get; set; }
        public List<DietPlan> MyPinnedDietPlans { get; set; }
    }
}
