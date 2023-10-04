using SmartOrderBy.Dtos;
using SmartOrderBy.Enums;
using SmartOrderBy.Extensions;
using System.Linq;
using System.Linq.Expressions;

namespace SmartOrderBy
{
    public static class SmartOrderBy
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource>(this IQueryable<TSource> query, Sorting orderBy)
            => Sorting(query, orderBy, OrderType.Order);

        public static IOrderedQueryable<TSource> ThenBy<TSource>(this IOrderedQueryable<TSource> query, Sorting orderBy)
            => Sorting(query, orderBy, OrderType.Then);

        private static IOrderedQueryable<TSource> Sorting<TSource>(IQueryable<TSource> query, Sorting sorting, OrderType orderType)
        {
            if (sorting.IsNull() || sorting.Name.IsNull())
                return query as IOrderedQueryable<TSource>;

            sorting = sorting.GetSortingByKey<TSource>();

            var entityType = typeof(TSource);

            var propertyList = entityType.PropertyInfos(sorting.Name).ToList();

            if (propertyList.IsNullOrNotAny())
                return query as IOrderedQueryable<TSource>;

            MemberExpression expression = null;

            var parameterExpression = Expression.Parameter(entityType, entityType.Name.ToLower());

            if (propertyList.Count == 1)
            {
                expression = Expression.Property(parameterExpression, propertyList.First().propertyInfo.Name);
            }
            else
            {
                var index = -1;

                MethodCallExpression firstOrDefaultExpression = null;

                foreach (var propertyInfo in propertyList)
                {
                    var propertyType = propertyInfo.propertyInfo.PropertyType;

                    if (propertyType.IsEnumarableType())
                    {
                        propertyType = propertyType.GetGenericArguments().FirstOrDefault();

                        var propertyExp = Expression.Property((Expression)expression ?? parameterExpression, propertyInfo.propertyInfo.Name);

                        firstOrDefaultExpression = Expression.Call(typeof(Enumerable), "FirstOrDefault", new[] { propertyType }, propertyExp);

                        if (propertyList.Count == index)
                            break;

                        index++;
                    }
                    else
                    {
                        if (firstOrDefaultExpression.IsNotNull())
                        {
                            expression = Expression.Property(firstOrDefaultExpression!, propertyInfo.propertyInfo.Name);
                            firstOrDefaultExpression = null;
                        }
                        else
                        {
                            expression = expression.IsNull()
                                ? Expression.Property(parameterExpression, propertyInfo.propertyInfo.Name)
                                : Expression.Property(expression!, propertyInfo.propertyInfo.Name);
                        }

                        if (propertyList.Count == index)
                            break;

                        index++;
                    }
                }
            }

            var sortType = sorting.GetSortType(orderType);

            var method = typeof(Queryable).GetMethods()
                .Where(m => m.Name == sortType && m.IsGenericMethodDefinition)
                .Where(m =>
                {
                    var parameters = m.GetParameters().ToList();
                    return parameters.Count == 2;
                }).Single();

            var genericMethod = method.MakeGenericMethod(entityType!, propertyList.LastOrDefault()!.propertyType);

            var newQuery = (IOrderedQueryable<TSource>)genericMethod.Invoke(genericMethod, new object[]
            {
                query,
                Expression.Lambda(expression!, parameterExpression)
            });

            return newQuery;
        }
    }
}
