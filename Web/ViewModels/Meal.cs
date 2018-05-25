using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class Meal
    {
        public int Id { get; set; }

        [Display(Name = "Meal Name")]
        [Required(ErrorMessage = "Meal name cannot be blank"), MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Meal information cannot be blank"), MaxLength(50)]
        [Display(Name = "Meal information")]
        public string Information { get; set; }

        [Display(Name = "Alarm")]
        public DateTime Alarm { get; set; }

        [Display(Name = "Reminder")]
        public bool Reminder;

        [Editable(false)]
        public int DietPlanId { get; set; }
    }
}
