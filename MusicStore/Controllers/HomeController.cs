using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;
using MusicStore.Models;
using System;
using System.Diagnostics;

namespace MusicStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StoreContext _context;
        public HomeController(ILogger<HomeController> logger, StoreContext context)
        {
            _logger = logger;
            _context = context;
    
        }

        public async Task<IActionResult> Index()
        {
            var albums = _context.Albums.OrderBy(a => Guid.NewGuid()).Include(a => a.Artist).Take(6);
            var album = _context.Albums.FirstOrDefault(a => a.AlbumID == 1);
            var cart = new ShoppingCart(HttpContext, _context);
            cart.AddToCart(album);
            return View(await albums.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}