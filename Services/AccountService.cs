
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TasksWebApi.Domains;
using TasksWebApi.Exceptions;
using TasksWebApi.Interfaces;
using TasksWebApi.Models;

namespace TasksWebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly TasksDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
       

        public AccountService(TasksDbContext context, IPasswordHasher<User> passwordHasher,AuthenticationSettings authenticationSettings)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            
        }

        public string GenerateJwt(LoginDto loginDto)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user is null)
            {
                throw new BadRequestException("Zła nazwa użytkownika lub złe hasło");
            }

           var result= _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if (result==PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Zła nazwa użytkownika lub złe hasło");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}"),
           

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credential = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credential);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        public void RegisterUser(RegisterUserDto userDto)
        {
           
            var isEmailTaken = _context.Users.Where(x => x.Email == userDto.Email);
            if (isEmailTaken.Any())
            {
                throw new MailTakenException("Ten adres mailowy jest już zajęty");
            }
            if (!userDto.Password.Equals(userDto.ConfirmPassword))
            {
                throw new ConfirmPasswordException("Hasła muszą być takie same");
            }

            var newUser = new User()
            {
                Email = userDto.Email,
                FirstName=userDto.FirstName,
                LastName=userDto.LastName
                
            };
          var hashedPassword= _passwordHasher.HashPassword(newUser, userDto.Password);
            newUser.PasswordHash = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
           
        }
    }
}
