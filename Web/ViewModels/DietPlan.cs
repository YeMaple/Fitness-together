using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class DietPlan
    {
        public int Id { get; set; }

        [Display(Name = "Plan Name")]
        [Required(ErrorMessage = "Plan name cannot be blank"), MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Plan information cannot be blank"), MaxLength(50)]
        [Display(Name = "Plan information")]
        public string Information { get; set; }

        [Display(Name = "Creator")]
        [Editable(false)]
        public string CreatorName { get; set; }

        [Editable(false)]
        public int PersonId { get; set; }

        public List<Meal> Meals { get; set; }
    }
}
