using Microsoft.EntityFrameworkCore;
using Mvc.Domain;

namespace Mvc.Infra;
public class Repo<T>(DbContext c) where T: Entity {
    private readonly DbContext db = c;
    private readonly DbSet<T> set = c.Set<T>();

    public async Task<IEnumerable<T>> GetAsync() => await set.ToListAsync();
    public async Task<T?> GetAsync(int? id) => (id is null) ? null : await set.FindAsync(id);
    public async Task AddAsync(T enitity) {
        set.Add(enitity);
        await db.SaveChangesAsync();
    }
    public async Task UpdateAsync(T enitity) {
        set.Update(enitity);
        await db.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id) {
        var x = await GetAsync(id);
        if (x is null) return;
        set.Remove(x);
        await db.SaveChangesAsync();
    }
}