// MIT License
// 
// Copyright (c) 2021 Alexey Politov, Yevgeny Khoroshavin
// https://github.com/EmptyBucket/Intervals
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

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