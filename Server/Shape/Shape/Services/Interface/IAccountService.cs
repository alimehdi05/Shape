using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shape.Requests;
namespace Shape.Services.Interface
{
    public interface IAccountService
    {
        Task<Tuple<bool, string>> Signup(SignupRequest request);
    }
}
