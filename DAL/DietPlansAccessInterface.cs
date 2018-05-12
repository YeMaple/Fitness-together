﻿using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface DietPlansAccessInterface
    {
        IEnumerable<DietPlans> GetDietPlansByCreatorId(int creator_id);
        IEnumerable<DietPlans> GetDietPlansByName(string name);
    }
}