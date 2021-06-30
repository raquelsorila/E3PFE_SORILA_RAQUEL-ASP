using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Data
{
    public interface IUserRepository
    {

        //Declaring Methods for implementing to the class
        AppUser Create(AppUser appUser);
        AppUser GetByEmail(string email);
        AppUser GetById(int id);
    }
}
