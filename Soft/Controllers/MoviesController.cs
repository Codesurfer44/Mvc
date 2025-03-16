using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mvc.Domain;
using Mvc.Soft.Data;
using Mvc.Infra;
using NuGet.Protocol;


namespace Mvc.Soft.Controllers;

public class MoviesController(ApplicationDbContext c) : Controller {
    private readonly ApplicationDbContext context = c;
    private readonly Repo<Movie> r = new(c);
    public async Task<IActionResult> Index() => View(await r.GetAsync());
    public async Task<IActionResult> Details(int? id)
    {
        var movie = await r.GetAsync(id);
        return (movie == null) ? NotFound() : View(movie);
    }
    public IActionResult Create() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Movie movie) {
        if (!ModelState.IsValid) return View(movie);
        await r.AddAsync(movie);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int? id) {
        var movie = await r.GetAsync(id);
        return (movie == null) ? NotFound() : View(movie);
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Movie movie) {
        if (id != movie.Id) return NotFound();
        if (!ModelState.IsValid) return View(movie);
        context.Movie.Update(movie);
        await r.UpdateAsync(movie);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id) {
        var movie = await r.GetAsync(id);
        return (movie == null) ? NotFound() : View(movie);
    }
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        await r.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
