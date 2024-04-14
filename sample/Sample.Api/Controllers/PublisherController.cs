namespace Sample.Api.Controllers;

[ApiController]
public class PublisherController : ControllerBase
{
    private readonly MemoryDbContext _context;

    public PublisherController(MemoryDbContext context)
    {
        _context = context;

        OrderByMapper.Map<Publisher, Book>("bookId", x => x.Id);
        OrderByMapper.Map<Publisher, Book, Author>("authorAge", x => x.Age);
        OrderByMapper.Map<Publisher, Book, Author, Country>("countryName", x => x.Name);
        OrderByMapper.Map<Publisher, Author, Language>("lanId", x => x.Id);
    }

    [HttpPost("/publishers")]
    public IActionResult GetPublishers(PublisherRequest request)
    {
        var result = _context.Publishers
            .Include(x => x.Books)
            .ThenInclude(x => x.Author)
            .OrderBy(request.OrderBy)
            .ToList();

        return Ok(result);
    }

    [HttpPost("/books")]
    public IActionResult GetBooks(BookSearchRequest request)
    {
        var result = _context.Books
            .Include(x => x.Author)
            .OrderBy(request.OrderBy)
            .ToList();

        return Ok(result);
    }
}