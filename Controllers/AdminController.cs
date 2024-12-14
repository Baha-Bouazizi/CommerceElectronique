using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommerceElectronique.Data;
using CommerceElectronique.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CommerceElectronique.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Categories()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Categories));
            }
            return View(category);
        }

        public IActionResult EditCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Categories));
            }
            return View(category);
        }

        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Categories));
        }


        // GET: /Admin/Products
        public IActionResult Products()
        {
            var products = _context.Products.Include(p => p.Category).ToList();
            return View(products);
        }
        // GET: /Admin/DetailsProduct/{id}
        public IActionResult DetailsProduct(int id)
        {
            var product = _context.Products
                                 .Include(p => p.Category)
                                 .FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: /Admin/AddProduct
        [HttpGet]
        public IActionResult AddProduct()
        {
            ViewBag.Categories = _context.Categories.ToList();
            return View();
        }

        // POST: /Admin/AddProduct
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product, IFormFile imageFile)
        {
            if (imageFile.Length > 0)
            {
                // Set the image path with the '/images/' prefix
                product.ImageUrl = "/images/" + Path.GetFileName(imageFile.FileName);

                var fileName = Path.GetFileName(imageFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

                var filePath = Path.Combine(uploadsFolder, fileName);

                Directory.CreateDirectory(uploadsFolder);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index","Home");
        }
        // GET: /Admin/EditProduct/{id}
        // GET: /Admin/EditProduct/{id}
        // GET: /Admin/EditProduct/{id}
        // GET: /Admin/EditProduct/{id}
        [HttpGet]
        public IActionResult EditProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = _context.Categories.ToList();

            return View(product);
        }


        [HttpPost]
        public async Task<IActionResult> EditProduct(int id, Product updatedProduct, IFormFile? imageFile)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Mise à jour des informations du produit
            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;
            product.Stock = updatedProduct.Stock;
            product.CategoryId = updatedProduct.CategoryId;

            if (imageFile != null && imageFile.Length > 0)
            {
                // Charger une nouvelle image
                var fileName = Path.GetFileName(imageFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                var filePath = Path.Combine(uploadsFolder, fileName);

                Directory.CreateDirectory(uploadsFolder);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Mettre à jour le chemin de l'image dans le produit
                product.ImageUrl = "/images/" + fileName;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product); // Une vue pour afficher la confirmation
        }
        [HttpPost]
        public async Task<IActionResult> DeleteProductConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            // Supprimer l'image associée, si nécessaire
            if (!string.IsNullOrEmpty(product.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home"); // Rediriger après la suppression
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult Orders()
        {
            var orders = _context.Orders
                                 .Include(o => o.User) // Charger les informations sur l'utilisateur
                                 .Where(o => o.Status == OrderStatus.Pending) // Seules les commandes en attente
                                 .OrderByDescending(o => o.OrderDate) // Trier les commandes par date (plus récentes en premier)
                                 .ToList();

            return View(orders); // Passer les commandes à la vue
        }

        public async Task<IActionResult> Details(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.User) // Inclure les informations de l'utilisateur
                .Include(o => o.CartItems) // Inclure les éléments du panier
                .ThenInclude(ci => ci.Product) // Inclure les produits des éléments du panier
                .FirstOrDefaultAsync(o => o.OrderId == orderId); // Chercher la commande par ID

            if (order == null)
            {
                return NotFound();
            }

            var orderViewModel = new OrderViewModel
            {
                OrderId = order.OrderId,
                OrderNumber = order.OrderNumber,
                UserName = order.User != null ? order.User.FirstName + " " + order.User.LastName : "No User", // Nom complet de l'utilisateur
                OrderDate = order.OrderDate,
                Status = order.Status,
                CartItems = order.CartItems.Select(ci => new CartItemViewModel
                {
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price,
                    Total = ci.Quantity * ci.Product.Price // Calcul du total pour chaque produit
                }).ToList(),
                TotalAmount = order.CartItems.Sum(ci => ci.Quantity * ci.Product.Price) // Calcul du montant total de la commande
            };

            return View(orderViewModel); // Passer les détails à la vue
        }





    }
}