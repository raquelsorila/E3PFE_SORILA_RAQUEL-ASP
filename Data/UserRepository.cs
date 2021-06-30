using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context; //the dbcontex allows us to connect to the database

        public UserRepository(UserContext context) 
        {
            _context = context; 
        }

        public AppUser Create( AppUser appUser) // using Add method to add a entry to the database
        {
            _context.AppUsers.Add(appUser);
            appUser.Id = _context.SaveChanges();

            return appUser;
        }

        public AppUser GetByEmail(string email) // using First or Default method to find a data using email to the database
        {
            return _context.AppUsers.FirstOrDefault(u => u.Email == email);
        }

        public AppUser GetById(int id) // using First or Default method to find a data using id to the database
        {
            return _context.AppUsers.FirstOrDefault(u => u.Id == id);
        }
    }
}
