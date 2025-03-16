using Microsoft.EntityFrameworkCore;
using Mvc.Domain;

namespace Mvc.Infra;
public class Repo<T>(DbContext c) where T: Entity {
    private readonly DbContext db = c;
    private readonly DbSet<T> set = c.Set<T>();

    public async Task<IEnumerable<T>> GetAsync() => await set.ToListAsync();
}