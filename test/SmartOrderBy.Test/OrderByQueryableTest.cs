namespace SmartOrderBy.Test;

public class OrderByQueryableTest
{
    private readonly IQueryable<Publisher> _publishersQuery = DataInitializer.FillMockDataQuery();

    public OrderByQueryableTest()
    {
        OrderByMapper.Map<Publisher, Book>("bookId", x => x.Id);
        OrderByMapper.Map<Publisher, Book, Author>("authorAge", x => x.Age);
        OrderByMapper.Map<Publisher, Book, Author, Country>("countryName", x => x.Name);
        OrderByMapper.Map<Publisher, Author, Language>("lanId", x => x.Id);
    }


    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Ascending_By_Id()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "Id",
                OrderType = "asc"
            }
        };

        //Act
        var result = _publishersQuery.OrderBy(request.OrderBy);

        //Assert
        Assert.Equal(1, result.FirstOrDefault()!.Id);
    }

    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Descending_By_Id()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "Id",
                OrderType = "desc"
            }
        };

        //Act
        var result = _publishersQuery.OrderBy(request.OrderBy);

        //Assert
        Assert.Equal(5, result.FirstOrDefault()!.Id);
    }

    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Ascending_By_Book_Id()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "bookId",
                OrderType = "asc"
            }
        };

        //Act
        var smartResult = _publishersQuery.OrderBy(request.OrderBy);
        var result = _publishersQuery.OrderBy(x => x.Books.FirstOrDefault()!.Id);

        //Assert
        Assert.True(smartResult.SelectMany(x => x.Books).FirstOrDefault()!.Id ==
                    result.SelectMany(x => x.Books).FirstOrDefault()!.Id);
    }

    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Descending_By_Book_Id()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "bookId",
                OrderType = "desc"
            }
        };

        //Act
        var smartResult = _publishersQuery.OrderBy(request.OrderBy);
        var result = _publishersQuery.OrderByDescending(x => x.Books.FirstOrDefault()!.Id);

        //Assert
        Assert.True(smartResult.SelectMany(x => x.Books).FirstOrDefault()!.Id ==
                    result.SelectMany(x => x.Books).FirstOrDefault()!.Id);
    }

    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Ascending_By_Author_Age()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "authorAge",
                OrderType = "asc"
            }
        };

        //Act
        var smartResult = _publishersQuery.OrderBy(request.OrderBy);
        var result = _publishersQuery.OrderBy(x => x.Books.FirstOrDefault()!.Author.Age);

        //Assert
        Assert.True(smartResult.SelectMany(x => x.Books).FirstOrDefault()!.Author.Age ==
                    result.SelectMany(x => x.Books).FirstOrDefault()!.Author.Age);
    }

    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Descending_By_Author_Age()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "authorAge",
                OrderType = "desc"
            }
        };

        //Act
        var smartResult = _publishersQuery.OrderBy(request.OrderBy);
        var result = _publishersQuery.OrderByDescending(x => x.Books.FirstOrDefault()!.Author.Age);

        //Assert
        Assert.True(smartResult.SelectMany(x => x.Books).Select(x => x.Author).FirstOrDefault()!.Age ==
                    result.SelectMany(x => x.Books).Select(x => x.Author).FirstOrDefault()!.Age);
    }

    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Ascending_By_Country_Name()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "countryName",
                OrderType = "asc"
            }
        };

        //Act
        var smartResult = _publishersQuery.OrderBy(request.OrderBy);
        var result = _publishersQuery.OrderBy(x => x.Books.FirstOrDefault()!.Author.Country.Name);

        //Assert
        Assert.True(smartResult.SelectMany(x => x.Books).FirstOrDefault()!.Author.Country.Name ==
                    result.SelectMany(x => x.Books).FirstOrDefault()!.Author.Country.Name);
    }

    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Descending_By_Country_Name()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "countryName",
                OrderType = "desc"
            }
        };

        //Act
        var smartResult = _publishersQuery.OrderBy(request.OrderBy);
        var result = _publishersQuery.OrderByDescending(x => x.Books.FirstOrDefault()!.Author.Country.Name);

        //Assert
        Assert.True(smartResult.SelectMany(x => x.Books).FirstOrDefault()!.Author.Country.Name ==
                    result.SelectMany(x => x.Books).FirstOrDefault()!.Author.Country.Name);
    }

    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Ascending_By_Language_Id()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "lanId",
                OrderType = "asc"
            }
        };

        //Act
        var smartResult = _publishersQuery.OrderBy(request.OrderBy);
        var result = _publishersQuery.OrderBy(x => x.MainAuthor.Languages.FirstOrDefault()!.Id);

        //Assert
        Assert.True(smartResult.Select(x => x.MainAuthor).SelectMany(a => a.Languages).FirstOrDefault()!.Id ==
                    smartResult.Select(x => x.MainAuthor).SelectMany(a => a.Languages).FirstOrDefault()!.Id);
    }

    [Fact]
    public void SmartOrderBy_Should_Return_Results_Order_By_Descending_By_Language_Id()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "lanId",
                OrderType = "desc"
            }
        };

        //Act
        var smartResult = _publishersQuery.OrderBy(request.OrderBy);
        var result = _publishersQuery.OrderByDescending(x => x.MainAuthor.Languages.FirstOrDefault()!.Id);

        //Assert
        Assert.True(smartResult.Select(x => x.MainAuthor).SelectMany(a => a.Languages).FirstOrDefault()!.Id ==
                    smartResult.Select(x => x.MainAuthor).SelectMany(a => a.Languages).FirstOrDefault()!.Id);
    }

    [Fact]
    public void ThenBy_Asc()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "Id",
                OrderType = "asc"
            },
            ThenBy = new Sorting
            {
                Name = "Name",
                OrderType = "desc"
            }
        };

        //Act
        var result = _publishersQuery
            .OrderBy(request.OrderBy)
            .ThenBy(request.ThenBy);

        //Assert
        Assert.Equal(1, result.FirstOrDefault()!.Id);
    }

    [Fact]
    public void ThenBy_Desc()
    {
        //Arrange
        PublisherRequest request = new()
        {
            OrderBy = new Sorting
            {
                Name = "Id",
                OrderType = "desc"
            },
            ThenBy = new Sorting
            {
                Name = "Name",
                OrderType = "desc"
            }
        };

        //Act
        var result = _publishersQuery
            .OrderBy(request.OrderBy)
            .ThenBy(request.ThenBy);

        //Assert
        Assert.Equal(5, result.FirstOrDefault()!.Id);
    }
}