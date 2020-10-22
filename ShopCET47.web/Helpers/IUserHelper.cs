using Microsoft.AspNetCore.Identity;
using ShopCET47.web.Data.Entities;
using ShopCET47.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.web.Helpers
{
    public interface IUserHelper
    {
        Task<User> GetUserByEmailAsync(string email);

        Task<IdentityResult> AddUserAsync(User user, string password);

        Task<SingInResult> LoginAsync(LoginViewModel model);

    }
}
