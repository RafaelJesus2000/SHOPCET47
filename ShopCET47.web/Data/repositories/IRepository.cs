using ShopCET47.web.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopCET47.web.Data.repositories
{
 
    //Nao esta a ser usado pois esta se a usar o generico
    public interface IRepository
    {
        void AddProduct(Product product);


        Product GetProduct(int id);


        IEnumerable<Product> GetProducts();


        bool ProductExists(int id);


        void RemoveProduct(Product product);


        Task<bool> SaveAllAsync();


        void UpdateProduct(Product product);


    }
}