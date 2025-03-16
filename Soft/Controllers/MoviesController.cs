using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Domain;
using Mvc.Soft.Data;

namespace Mvc.Soft.Controllers;

public class MoviesController(ApplicationDbContext c) : Controller {
    private readonly ApplicationDbContext context = c;
    public async Task<IActionResult> Index() => View(await context.Movie.ToListAsync());
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();
        var movie = await context.Movie.FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null) return NotFound();
        return View(movie);
    }
    public IActionResult Create() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Movie movie) {
        if (!ModelState.IsValid) return View(movie);
        context.Add(movie);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int? id) {
        if (id == null) return NotFound();
        var movie = await context.Movie.FindAsync(id);
        if (movie == null) return NotFound();
        return View(movie);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Movie movie) {
        if (id != movie.Id) return NotFound();
        if (!ModelState.IsValid) return View(movie);
        try {
            context.Update(movie);
            await context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (!MovieExists(movie.Id)) return NotFound();
            else throw;
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id) {
        if (id == null) return NotFound();
        var movie = await context.Movie.FirstOrDefaultAsync(m => m.Id == id);
        if (movie == null) return NotFound();
        return View(movie);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        var movie = await context.Movie.FindAsync(id);
        if (movie != null) context.Movie.Remove(movie);
        await context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    private bool MovieExists(int id) => context.Movie.Any(e => e.Id == id);
}
