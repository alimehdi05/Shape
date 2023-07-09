using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Shape.Services.Interface;
using Shape.DTOs;
using Shape.Requests;
using Shape.Context;
using System.Security.Cryptography;
using System.Diagnostics.Eventing.Reader;

namespace Shape.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _config;
        public AccountService(IConfiguration configuration)
        {
            this._config = configuration;

        }


        public async Task<Tuple<bool, string>> Signup(SignupRequest request)
        {
            try
            {

                using (var db = new ShapeDbContext())
                {
                    using (var transaction = await db.Database.BeginTransactionAsync())
                    {
                        try
                        {
                            request.Email = request.Email.Trim();

                            if (await db.Users.AnyAsync(x => x.Email.ToLower() == request.Email.ToLower()))
                            {
                                return Tuple.Create(false, "Email already exists");
                            }
                       //     if (!ValidatePassword(request.Password)) { return Tuple.Create(false, "Incorrect Password Format"); }
                            var passwordHash = HashPassword(request.Password);

                            var user = new User
                            {
                                FirstName = request.FirstName,
                                LastName = request.LastName,
                                Email = request.Email.ToLower(),
                                Password = passwordHash,
                                IsActive = true,
                                CreatedOn = DateTime.UtcNow.AddHours(-8),
                                IsDeleted = false

                            };
                            await db.Users.AddAsync(user);

                           
                            await db.SaveChangesAsync();

                            await transaction.CommitAsync();

                            return Tuple.Create(true, "Account created successfully!");
                        }
                        catch (Exception ex)
                        {
                            await transaction.RollbackAsync();
                            throw ex;
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //private bool ValidatePassword(string passwd)
        //{
        //    if (passwd.Length < 8)
        //        return false;
        //    if (!passwd.Any(char.IsUpper))
        //        return false;
        //    if (!passwd.Any(char.IsLower))
        //        return false;
        //    if (passwd.Contains(" "))
        //        return false;
        //    string specialCh = @"[$&+,:;=?@#|'<>.-^*()%!]";
        //    char[] specialChArray = specialCh.ToCharArray();
        //    foreach (char ch in specialChArray)
        //    {
        //        if (passwd.Contains(ch))
        //            return false;
        //    }
        //    return true;
        //}
    }

}

