using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Dtos;
using Api.Helpers;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route(template:"api")] // setting the endpoint of the api
    [ApiController] // Means that the controller is being used for an api
    public class AuthController : ControllerBase //dependency injection for the controller
    {
        private readonly IUserRepository _repository;  // connecting the repository for authentication
        private readonly JwtService _jwtService; //connectiong the helper made to process jwt

        public AuthController(IUserRepository repository, JwtService jwtService) //constructor of the controller
        {
            _repository = repository;  //instance of the repository
            _jwtService = jwtService;   //instance of the jwt helper
        }

        [HttpPost(template: "register")] // post request
        public IActionResult Register(RegisterDto dto) // Register function
        {

            //creating a AppUser object using the passed properties of the dto(data transfer object)
            var user = new AppUser
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password) // encrypting password using a nuget package BCrypt to turn a string into a hash
            };

            return Created("success", _repository.Create(user)); // success message and returns the user to the client as a response
        }

        [HttpPost(template:"login")] // post request
        public IActionResult Login(LoginDto dto) // Login Function
        {
            var user = _repository.GetByEmail(dto.Email); //finding if user exists by using email

            if (user == null) return BadRequest(new { message = "Invalid Credentials" }); //if user is null,it means the email isnt associated to any account in the database

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))//will turn the string password into encrypted hash and then checking if it matches the one in the db
            {
                return BadRequest(new { message = "Invalid Credentials" }); // error message 
            }

            var jwt = _jwtService.Generate(user.Id); // generating jwt token using the Generate method in the jwt helper

            Response.Cookies.Append("jwt", jwt, new CookieOptions // adding the jwt token to the cookies
            {
                HttpOnly = true,
            });

            return Ok(new // success message
            { 
                message = "success"
            });
        }

        [HttpGet(template:"user")] // finding user
        public IActionResult User()
        {
            try
            {
                var jwt = Request.Cookies["jwt"]; // request cookies for jwt
                var token = _jwtService.Verify(jwt); // verify if the token matches

                int userId = int.Parse(token.Issuer); // getting the id from issuer

                var user = _repository.GetById(userId); //finding if the user exists using the id

                return Ok(user);
            }
            catch(Exception e) // catching error
            {
                return Unauthorized();
            }
            
        }

        [HttpPost(template:"logout")] // Logout function
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt"); //Removes the jwt token from the cookie

            return Ok(new // success message
            {
                message = "success"
            });
        }
    }
}
