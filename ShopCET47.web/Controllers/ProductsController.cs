using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopCET47.web.Data.Entities;
using ShopCET47.web.Data.repositories;
using ShopCET47.web.Helpers;
using ShopCET47.web.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ShopCET47.web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productrepository;
        private readonly IUserHelper _userHelper;

        public ProductsController(IProductRepository productrepository, IUserHelper userHelper)
        {
            _productrepository = productrepository;
            _userHelper = userHelper;
        }





        // GET: Products
        public IActionResult Index()
        {
            return View(_productrepository.GetAll());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productrepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            var view = this.ToProductDoModel(product);

            return View(view);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Name,Price,ImageFile,LastPurchase,LastSale,IsAvailable,Stock")] ProductDoModel view)
        {
            if (ModelState.IsValid)
            {



                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";


                    path = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Products",

                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Products/{file}";
                }


                var product = this.ToProduct(view, path);

                //TODO: Mudar para o user que depois tiver logado
                product.User = await _userHelper.GetUserByEmailAsync("rafael.neves.jesus@gmail.com");

                await _productrepository.CreateAsync(product);


                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productrepository.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            var view = this.ToProductDoModel(product);

            return View(view);
        }

        private ProductDoModel ToProductDoModel(Product product)
        {
            return new ProductDoModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                IsAvailable = product.IsAvailable,
                LastPurchase = product.LastPurchase,
                LastSale = product.LastSale,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                User = product.User
            };
        }

        private Product ToProduct(ProductDoModel view, string path)
        {
            return new Product
            {
                Id = view.Id,
                ImageUrl = path,
                IsAvailable = view.IsAvailable,
                LastPurchase = view.LastPurchase,
                LastSale = view.LastSale,
                Name = view.Name,
                Price = view.Price,
                Stock = view.Stock,
                User = view.User
            };
        }


        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,ImageFile,LastPurchase,LastSale,IsAvailable,Stock")] ProductDoModel view)
        {
            if (id != view.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {

                    var path = view.ImageUrl;


                        if (view.ImageFile != null && view.ImageFile.Length > 0)
                        {
                            path = string.Empty;

                            var guid = Guid.NewGuid().ToString();
                            var file = $"{guid}.jpg";


                            path = Path.Combine(
                                Directory.GetCurrentDirectory(),
                                "wwwroot\\images\\Products",
                                file);

                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await view.ImageFile.CopyToAsync(stream);
                            }

                            path = $"~/images/Products/{file}";
                        }
                    

                    var product = this.ToProduct(view, path);

                    //TODO: Mudar para o user que depois tiver logado
                    product.User = await _userHelper.GetUserByEmailAsync("rafael.neves.jesus@gmail.com");

                    await _productrepository.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _productrepository.ExistAsync(view.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productrepository.GetByIdAsync(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productrepository.GetByIdAsync(id);
            await _productrepository.DeleteAsync(product);
            return RedirectToAction(nameof(Index));
        }


    }
}
