using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicStore.Data;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;
        public StoreController(StoreContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> ListGenres()
        {

            var genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return View(genres);

        }

        public async Task<IActionResult> ListAlbums(int? id)
        {
            var storeContext = _context.Albums.Where(a => a.GenreID == id).Include(a => a.Artist).Include(a => a.Genre);
            return View(await storeContext.ToListAsync());
        }
    }
}
