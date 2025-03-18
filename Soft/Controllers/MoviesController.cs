using Mvc.Domain;
using Mvc.Soft.Data;

namespace Mvc.Soft.Controllers;

public class MoviesController(ApplicationDbContext c) : BaseController<Movie>(c) { }