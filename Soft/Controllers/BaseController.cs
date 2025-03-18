using Microsoft.AspNetCore.Mvc;
using Mvc.Domain;
using Mvc.Infra;
using Microsoft.EntityFrameworkCore;


namespace Mvc.Soft.Controllers;

public class BaseController<T>(DbContext c) : Controller where T : Entity {
    private readonly Repo<T> r = new(c);
    private async Task<IActionResult> showAsync(string viewName, int? id) {
        var x = await r.GetAsync(id);
        return (x == null) ? NotFound() : View(viewName, x);
    }
    public async Task<IActionResult> Index() => View(await r.GetAsync());
    public async Task<IActionResult> Details(int? id) => await showAsync(nameof (Details), id);
    public IActionResult Create() => View();
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(T entity) {
        if (!ModelState.IsValid) return View(entity);
        await r.AddAsync(entity);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Edit(int? id) => await showAsync(nameof (Edit), id);
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, T entity) {
        if (id != entity.Id) return NotFound();
        if (!ModelState.IsValid) return View(entity);
        await r.UpdateAsync(entity);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id) => await showAsync(nameof (Delete), id);
    [HttpPost, ActionName("Delete"), ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id) {
        await r.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}