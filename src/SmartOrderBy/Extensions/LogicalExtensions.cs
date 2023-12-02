namespace SmartOrderBy.Extensions;

public static class LogicalExtensions
{
    internal static bool IsNull<T>(this T item) where T : class => item is null;

    internal static bool IsNotNull<T>(this T item) where T : class => item is not null;

    internal static bool IsNotNullAndAny<T>(this IEnumerable<T> items) => items is not null && items.Any();

    internal static bool IsNullOrNotAny<T>(this IEnumerable<T> items) => items is null || !items.Any();

    internal static bool IsEnumarableType(this Type type) =>
        type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(IEnumerable<>) || type.GetGenericTypeDefinition() == typeof(List<>));
}