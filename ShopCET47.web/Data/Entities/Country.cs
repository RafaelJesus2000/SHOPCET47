using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShopCET47.web.Data.Entities;

namespace ShopCET47.web.Data.Entities
{
    public class Country : identity
    {
        public int id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Name { get; set; }

    }
}
