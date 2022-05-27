using System.Collections;

namespace Intervals.Tests.Utils;

public partial class EnumerableExtensionsTests
{
    private static IEnumerable MergeAscending_WhenBothHasItems_Data()
    {
        yield return new[]
        {
            new[] { 0, 2, 4, 6, 8 },
            new[] { 1, 3, 5, 7, 9 },
            new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }
        };
        yield return new[]
        {
            new[] { 0 },
            new[] { 1, 3, 5, 7, 9 },
            new[] { 0, 1, 3, 5, 7, 9 }
        };
        yield return new[]
        {
            new[] { 0, 2, 4, 6, 8 },
            new[] { 1 },
            new[] { 0, 1, 2, 4, 6, 8 }
        };
    }
}