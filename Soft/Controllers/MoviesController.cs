using Microsoft.AspNetCore.Mvc;
using Mvc.Domain;
using Mvc.Soft.Data;
using Mvc.Infra;


namespace Mvc.Soft.Controllers;

public class MoviesController(ApplicationDbContext c) : Controller {
    private readonly Repo<Movie> r = new(c);
    private async Task<IActionResult> showAsync(string viewName, int? id) {
        var x = await r.GetAsync(id);
        return (x == null) ? NotFound() : View(viewName, x);
    }
    public async Task<IActionResult> Index() => View(await r.GetAsync());
    public async Task<IActionResult> Details(int? id) => await showAsync(nameof (Details), id);
    public IActionResult Create() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Movie movie) {
        if (!ModelState.IsValid) return View(movie);
        await r.AddAsync(movie);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int? id) => await showAsync(nameof (Edit), id);
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Movie movie) {
        if (id != movie.Id) return NotFound();
        if (!ModelState.IsValid) return View(movie);
        await r.UpdateAsync(movie);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id) => await showAsync(nameof (Delete), id);
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        await r.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
