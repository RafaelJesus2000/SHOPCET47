using Microsoft.AspNetCore.Identity;
using ShopCET47.web.Data.Entities;
using ShopCET47.web.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;


        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            var user = await _userHelper.GetUserByEmailAsync("rafael.neves.jesus@gmail.com");
            if(user==null)
            {
                user = new User
                {
                    FirstName = "rafael",
                    LastName = "jesus",
                    Email = "rafael.neves.jesus@gmail.com",
                    UserName = "rafael.neves.jesus@gmail.com",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                
                if(result!=IdentityResult.Success)
                {
                    throw new InvalidOperationException("could not creat the user in seeder");
                }
            }

            if(! _context.Products.Any())
            {
                this.AddProduct("iPhone X",user);
                this.AddProduct("Rato Mickey",user);
                this.AddProduct("iWatch serie 4",user);
                this.AddProduct("millennium falcon",user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, User user)
        {
            _context.Products.Add(new Entities.Product
            {
                Name = name,
                Price = _random.Next(1000),
                IsAvailable = true,
                Stock = _random.Next(100),
                User = user
            }) ;
        }
    }
}
