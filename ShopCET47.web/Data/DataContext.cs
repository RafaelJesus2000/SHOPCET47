﻿using Microsoft.EntityFrameworkCore;
using ShopCET47.web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
    }
}
