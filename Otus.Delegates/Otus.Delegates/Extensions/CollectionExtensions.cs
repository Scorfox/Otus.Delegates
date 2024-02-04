namespace Otus.Delegates.Extensions;

public static class CollectionExtensions
{
    /// <summary>
    /// Обобщённая функция расширения, находящая и возвращающая максимальный элемент коллекции.
    /// </summary>
    /// <param name="collection">IEnumerable collection</param>
    /// <param name="convertToNumber">Делегат, преобразующий входной тип в число для возможности поиска максимального значения</param>
    /// <typeparam name="T">Generic type</typeparam>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static T? GetMax<T>(this IEnumerable<T?> collection, Func<T, float> convertToNumber) where T : class
    {
        var enumerable = collection as T[] ?? collection.ToArray();
        if (collection == null || !enumerable.OfType<object>().Any())
        {
            throw new ArgumentException("Collection cannot be null or empty.");
        }

        T? maxElement = null;
        var maxValue = float.MinValue;

        foreach (var item in enumerable)
        {
            var convertedValue = convertToNumber(item!);

            if (convertedValue > maxValue)
            {
                maxValue = convertedValue;
                maxElement = item;
            }
        }

        return maxElement;
    }
}