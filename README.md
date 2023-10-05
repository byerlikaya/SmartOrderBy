# SmartOrderBy
It intelligently sorts a sequence of items in a simple and practical way.

![GitHub Workflow Status (with event)](https://img.shields.io/github/actions/workflow/status/byerlikaya/SmartOrderBy/dotnet.yml)
[![SmartOrderBy Nuget](https://img.shields.io/nuget/v/SmartOrderBy)](https://www.nuget.org/packages/SmartOrderBy)
[![SmartOrderBy Nuget](https://img.shields.io/nuget/dt/SmartOrderBy)](https://www.nuget.org/packages/SmartOrderBy)

**SmartOrderBy** is a method that aims to make the `Queryable.OrderBy` method smarter and is based on the foundations of .NET.

I would be very happy if you could star ⭐ the project.

#### Quick Start
The usage of **SmartOrderBy** is quite simple.

1. Install `SmartOrderBy` NuGet package from [here](https://www.nuget.org/packages/SmartOrderBy/).

````
PM> Install-Package SmartOrderBy
````

2. We add our Sorting object to our Request object.

```csharp
public Sorting OrderBy { get; set; }
```
3. And we sort intelligently. And that's it.

```csharp
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
```
#### Details

If you want to specify the name of the field you want to sort differently from the field in the entity, you need to map it.

👉 You can access the sample domain structure [here](https://github.com/byerlikaya/SmartOrderBy/tree/main/sample/Sample.Common/Entity). 👈

For example, if you want to make a sorting with the name `bookId` according to the Id field of the `Book` entity in `Publisher`, you will need to make a mapping as follows.

```csharp
OrderByMapper.Map<Publisher, Book>("bookId", x => x.Id);
```
Or if you want to make a sort with the `authorAge` name according to the Age field of the `Author` entity in the Book entity in `Publisher`;

```csharp
OrderByMapper.Map<Publisher, Book, Author>("authorAge", x => x.Age);
```
⭐ The important thing here is to specify the relevant entities in Map<TSource,T1,T2,...> respectively until you reach the sort field.


