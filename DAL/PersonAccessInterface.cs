using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface PersonAccessInterface
    {
        Persons Login(String email, String password);
    }
}
