namespace SmartOrderBy.Extensions;

public static class OrderByMapper
{
    private static List<OrderByMap> MapList { get; } = new();

    public static void Map<TSource, T1, T2, T3, T4>(string key, Expression<Func<T4, object>> selector) =>
        Map(key, typeof(TSource).Name, $"{typeof(T1).Name}.{typeof(T2).Name}.{typeof(T3).Name}.{typeof(T4).Name}", selector);

    public static void Map<TSource, T1, T2, T3>(string key, Expression<Func<T3, object>> selector) =>
        Map(key, typeof(TSource).Name, $"{typeof(T1).Name}.{typeof(T2).Name}.{typeof(T3).Name}", selector);

    public static void Map<TSource, T1, T2>(string key, Expression<Func<T2, object>> selector) =>
        Map(key, typeof(TSource).Name, $"{typeof(T1).Name}.{typeof(T2).Name}", selector);

    public static void Map<TSource, T1>(string key, Expression<Func<T1, object>> selector) =>
        Map(key, typeof(TSource).Name, $"{typeof(T1).Name}", selector);

    public static void Map<TSource, T>(string key, string map, Expression<Func<T, object>> selector) =>
        Map(key, typeof(TSource).Name, $"{map}", selector);

    public static void Map<TSource>(string key, Expression<Func<TSource, object>> selector) =>
        Map(key, typeof(TSource).Name, $"{typeof(TSource).Name}", selector);

    private static void Map<T>(string key, string mainEntity, string map, Expression<Func<T, object>> selector)
    {
        if (selector.IsNull())
            return;

        var memberName = selector.Body.GetMemberName();

        if (MapList.Exists(x => x.EntityName == mainEntity))
        {
            var orderByMap = MapList.Find(x => x.EntityName == mainEntity);

            if (orderByMap!.Maps.TrueForAll(x => x.Key != key))
                orderByMap.Maps.Add(new Map
                {
                    Key = key,
                    Mapping = $"{map}.{memberName}"
                });
        }
        else
        {
            MapList.Add(new OrderByMap
            {
                EntityName = mainEntity,
                Maps =
                [
                    new Map
                    {
                        Key = key,
                        Mapping = $"{map}.{memberName}"
                    }
                ]
            });
        }
    }

    internal static Sorting GetSortingByKey<TSource>(this Sorting sorting)
    {
        if (MapList.IsNullOrNotAny())
            return sorting;

        if (!MapList.Exists(x => x.Maps.Exists(e => e.Key == sorting.Name)))
            return sorting;

        var map = MapList.Find(x => x.EntityName == typeof(TSource).Name && x.Maps.Exists(e => e.Key == sorting.Name));

        if (map.IsNull())
            return sorting;

        var entityDto = map!.Maps.Find(x => x.Key == sorting.Name);

        return new Sorting
        {
            OrderType = sorting.OrderType,
            Name = entityDto!.Mapping
        };
    }
}