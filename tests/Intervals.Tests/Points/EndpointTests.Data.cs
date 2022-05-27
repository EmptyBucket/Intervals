using System.Collections;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests.Points;

public partial class EndpointTests
{
    public static IEnumerable CompareTo_WhenHasSameValue_Data()
    {
        var excludedPoint = Point.Excluded(0);
        var includedPoint = Point.Included(0);
        var leftExclude = Endpoint.Left(excludedPoint);
        var rightExclude = Endpoint.Right(excludedPoint);
        var leftInclude = Endpoint.Left(includedPoint);
        var rightInclude = Endpoint.Right(includedPoint);

        yield return new TestCaseData(leftExclude, rightExclude, 1)
            .SetName("WhenHasSameValueAndExcludedLeftAndExcludedRight_ReturnPositive");
        yield return new TestCaseData(leftInclude, rightExclude, 1)
            .SetName("WhenHasSameValueAndIncludedLeftAndExcludedRight_ReturnPositive");
        yield return new TestCaseData(leftExclude, rightInclude, 1)
            .SetName("WhenHasSameValueAndExcludedLeftAndIncludedRight_ReturnPositive");
        yield return new TestCaseData(rightInclude, leftInclude, 1)
            .SetName("WhenHasSameValueAndIncludedRightAndIncludedLeft_ReturnPositive");

        yield return new TestCaseData(rightExclude, leftExclude, -1)
            .SetName("WhenHasSameValueAndExcludedRightAndExcludedLeft_ReturnNegative");
        yield return new TestCaseData(rightExclude, leftInclude, -1)
            .SetName("WhenHasSameValueAndExcludedRightAndIncludedLeft_ReturnNegative");
        yield return new TestCaseData(rightInclude, leftExclude, -1)
            .SetName("WhenHasSameValueAndIncludedRightAndExcludedLeft_ReturnNegative");
        yield return new TestCaseData(leftInclude, rightInclude, -1)
            .SetName("WhenHasSameValueAndIncludedLeftAndIncludedRight_ReturnNegative");

        yield return new TestCaseData(leftExclude, leftExclude, 0)
            .SetName("WhenHasSameValueAndBothExcludedLeft_ReturnZero");
        yield return new TestCaseData(rightExclude, rightExclude, 0)
            .SetName("WhenHasSameValueAndBothExcludedRight_ReturnZero");
        yield return new TestCaseData(leftInclude, leftInclude, 0)
            .SetName("WhenHasSameValueAndBothIncludedLeft_ReturnZero");
        yield return new TestCaseData(rightInclude, rightInclude, 0)
            .SetName("WhenHasSameValueAndBothIncludedRight_ReturnZero");
        
        yield return new TestCaseData(leftExclude, leftInclude, 1)
            .SetName("WhenHasSameValueAndExcludedLeftAndIncludedLeft_ReturnPositive");
        yield return new TestCaseData(rightInclude, rightExclude, 1)
            .SetName("WhenHasSameValueAndIncludedRightAndExcludedRight_ReturnPositive");
        yield return new TestCaseData(rightExclude, rightInclude, -1)
            .SetName("WhenHasSameValueAndExcludedRightAndIncludedRight_ReturnNegative");
        yield return new TestCaseData(leftInclude, leftExclude, -1)
            .SetName("WhenHasSameValueAndIncludedLeftAndExcludedRight_ReturnNegative");
    }
}