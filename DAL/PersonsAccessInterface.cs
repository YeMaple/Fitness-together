using System;
using System.Collections.Generic;
using System.Text;
using DAL.Models;

namespace DAL
{
    public interface PersonsAccessInterface
    {
        Persons Login(String email, String password);
    }
}
