namespace DiplomaChat.Common.Infrastructure.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T?> ToNullable<T>(this IEnumerable<T> enumerable)
        where T : struct
    {
        return enumerable.Select(value => new T?(value)).ToArray();
    }
}