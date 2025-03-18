using Mvc.Domain;
using Mvc.Soft.Data;

namespace Mvc.Soft.Controllers;

public class BooksController(ApplicationDbContext c) : BaseController<Books>(c) { }
