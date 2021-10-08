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
using Intervals.Intervals;
using Moq;
using NUnit.Framework;

namespace Intervals.Test
{
	public class EndpointCompareToSameValueData : IEnumerable
	{
		public IEnumerator GetEnumerator()
		{
			var excludedPoint = new Mock<IPoint<int>>();
			excludedPoint.Setup(p => p.Value).Returns(0);
			excludedPoint.Setup(p => p.Inclusion).Returns(Inclusion.Excluded);
			var includedPoint = new Mock<IPoint<int>>();
			includedPoint.Setup(p => p.Value).Returns(0);
			includedPoint.Setup(p => p.Inclusion).Returns(Inclusion.Included);

			yield return new TestCaseData(
					Endpoint.New(includedPoint.Object, EndpointLocation.Left),
					Endpoint.New(includedPoint.Object, EndpointLocation.Left), 0)
				.SetName("AndBothIncludedLeft_ReturnZero");
			yield return new TestCaseData(
					Endpoint.New(includedPoint.Object, EndpointLocation.Right),
					Endpoint.New(includedPoint.Object, EndpointLocation.Right), 0)
				.SetName("AndBothIncludedRight_ReturnZero");
			yield return new TestCaseData(
					Endpoint.New(includedPoint.Object, EndpointLocation.Left),
					Endpoint.New(includedPoint.Object, EndpointLocation.Right), -1)
				.SetName("AndIncludedLeftAndIncludedRight_ReturnZero");
			yield return new TestCaseData(
					Endpoint.New(includedPoint.Object, EndpointLocation.Right),
					Endpoint.New(includedPoint.Object, EndpointLocation.Left), 1)
				.SetName("AndIncludedRightAndIncludedLeft_ReturnZero");
			yield return new TestCaseData(
					Endpoint.New(includedPoint.Object, EndpointLocation.Left),
					Endpoint.New(excludedPoint.Object, EndpointLocation.Left), -1)
				.SetName("AndIncludedLeftAndExcludedRight_ReturnNegative");
			yield return new TestCaseData(
					Endpoint.New(includedPoint.Object, EndpointLocation.Right),
					Endpoint.New(excludedPoint.Object, EndpointLocation.Right), 1)
				.SetName("AndIncludedRightAndExcludedRight_ReturnPositive");
			yield return new TestCaseData(
					Endpoint.New(includedPoint.Object, EndpointLocation.Left),
					Endpoint.New(excludedPoint.Object, EndpointLocation.Right), 1)
				.SetName("AndIncludedLeftAndExcludedRight_ReturnPositive");
			yield return new TestCaseData(
					Endpoint.New(includedPoint.Object, EndpointLocation.Right),
					Endpoint.New(excludedPoint.Object, EndpointLocation.Left), -1)
				.SetName("AndIncludedRightAndExcludedLeft_ReturnNegative");

			yield return new TestCaseData(
					Endpoint.New(excludedPoint.Object, EndpointLocation.Left),
					Endpoint.New(excludedPoint.Object, EndpointLocation.Left), 0)
				.SetName("AndBothExcludedLeft_ReturnZero");
			yield return new TestCaseData(
					Endpoint.New(excludedPoint.Object, EndpointLocation.Right),
					Endpoint.New(excludedPoint.Object, EndpointLocation.Right), 0)
				.SetName("AndBothExcludedRight_ReturnZero");
			yield return new TestCaseData(
					Endpoint.New(excludedPoint.Object, EndpointLocation.Left),
					Endpoint.New(excludedPoint.Object, EndpointLocation.Right), 1)
				.SetName("AndExcludedLeftAndExcludedRight_ReturnPositive");
			yield return new TestCaseData(
					Endpoint.New(excludedPoint.Object, EndpointLocation.Right),
					Endpoint.New(excludedPoint.Object, EndpointLocation.Left), -1)
				.SetName("AndExcludedRightAndExcludedLeft_ReturnNegative");
			yield return new TestCaseData(
					Endpoint.New(excludedPoint.Object, EndpointLocation.Left),
					Endpoint.New(includedPoint.Object, EndpointLocation.Left), 1)
				.SetName("AndExcludedLeftAndIncludedLeft_ReturnPositive");
			yield return new TestCaseData(
					Endpoint.New(excludedPoint.Object, EndpointLocation.Right),
					Endpoint.New(includedPoint.Object, EndpointLocation.Right), -1)
				.SetName("AndExcludedRightAndIncludedRight_ReturnNegative");
			yield return new TestCaseData(
					Endpoint.New(excludedPoint.Object, EndpointLocation.Left),
					Endpoint.New(includedPoint.Object, EndpointLocation.Right), 1)
				.SetName("AndExcludedLeftAndIncludedRight_ReturnPositive");
			yield return new TestCaseData(
					Endpoint.New(excludedPoint.Object, EndpointLocation.Right),
					Endpoint.New(includedPoint.Object, EndpointLocation.Left), -1)
				.SetName("AndExcludedRightAndIncludedLeft_ReturnNegative");
		}
	}
}