namespace Sample.Api;

public static class PublisherData
{
    public static void FillDummyData()
    {
        PublisherRepository publisherRepository = new();
        var publisher = DataInitializer.FillMockDataQuery().ToList();
        publisherRepository.AddRange(publisher);
    }
}