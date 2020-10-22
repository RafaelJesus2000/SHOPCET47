using ShopCET47.web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopCET47.web.Data.repositories
{
    public class Repository : IRepository
    {
        private readonly DataContext _context;

        public Repository(DataContext context)
        {
            _context = context;
        }

        //metodo para ir buscar os produtos todos

        public IEnumerable<Product> GetProducts()
        {
            return _context.Products.OrderBy(P => P.Name);
        }

        //metodo para ir buscar um produto pelo id

        public Product GetProduct(int id)
        {
            return _context.Products.Find(id);
        }

        //metodo que adiciona um produto à tabela 
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
        }

        //metodo que atualiza o produto
        public void UpdateProduct(Product product)
        {
            _context.Update(product);
        }

        //metodo para apagar o produto
        public void RemoveProduct(Product product)
        {
            _context.Products.Remove(product);
        }


        //metodo que atualiza a base de dados
        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        //verifica se o produto existe
        public bool ProductExists(int id)
        {
            return _context.Products.Any(p => p.Id == id);
        }

    }
}
