using System.Collections;
using Intervals.Points;
using NUnit.Framework;

namespace Intervals.Tests;

public class EndpointCompareToSameValueData : IEnumerable
{
	public IEnumerator GetEnumerator()
	{
		var excludedPoint = Point.Excluded(0);
		var includedPoint = Point.Included(0);

		yield return new TestCaseData(
				new Endpoint<int>(includedPoint, EndpointLocation.Left),
				new Endpoint<int>(includedPoint, EndpointLocation.Left), 0)
			.SetName("AndBothIncludedLeft_ReturnZero");
		yield return new TestCaseData(
				new Endpoint<int>(includedPoint, EndpointLocation.Right),
				new Endpoint<int>(includedPoint, EndpointLocation.Right), 0)
			.SetName("AndBothIncludedRight_ReturnZero");
		yield return new TestCaseData(
				new Endpoint<int>(includedPoint, EndpointLocation.Left),
				new Endpoint<int>(includedPoint, EndpointLocation.Right), -1)
			.SetName("AndIncludedLeftAndIncludedRight_ReturnZero");
		yield return new TestCaseData(
				new Endpoint<int>(includedPoint, EndpointLocation.Right),
				new Endpoint<int>(includedPoint, EndpointLocation.Left), 1)
			.SetName("AndIncludedRightAndIncludedLeft_ReturnZero");
		yield return new TestCaseData(
				new Endpoint<int>(includedPoint, EndpointLocation.Left),
				new Endpoint<int>(excludedPoint, EndpointLocation.Left), -1)
			.SetName("AndIncludedLeftAndExcludedRight_ReturnNegative");
		yield return new TestCaseData(
				new Endpoint<int>(includedPoint, EndpointLocation.Right),
				new Endpoint<int>(excludedPoint, EndpointLocation.Right), 1)
			.SetName("AndIncludedRightAndExcludedRight_ReturnPositive");
		yield return new TestCaseData(
				new Endpoint<int>(includedPoint, EndpointLocation.Left),
				new Endpoint<int>(excludedPoint, EndpointLocation.Right), 1)
			.SetName("AndIncludedLeftAndExcludedRight_ReturnPositive");
		yield return new TestCaseData(
				new Endpoint<int>(includedPoint, EndpointLocation.Right),
				new Endpoint<int>(excludedPoint, EndpointLocation.Left), -1)
			.SetName("AndIncludedRightAndExcludedLeft_ReturnNegative");

		yield return new TestCaseData(
				new Endpoint<int>(excludedPoint, EndpointLocation.Left),
				new Endpoint<int>(excludedPoint, EndpointLocation.Left), 0)
			.SetName("AndBothExcludedLeft_ReturnZero");
		yield return new TestCaseData(
				new Endpoint<int>(excludedPoint, EndpointLocation.Right),
				new Endpoint<int>(excludedPoint, EndpointLocation.Right), 0)
			.SetName("AndBothExcludedRight_ReturnZero");
		yield return new TestCaseData(
				new Endpoint<int>(excludedPoint, EndpointLocation.Left),
				new Endpoint<int>(excludedPoint, EndpointLocation.Right), 1)
			.SetName("AndExcludedLeftAndExcludedRight_ReturnPositive");
		yield return new TestCaseData(
				new Endpoint<int>(excludedPoint, EndpointLocation.Right),
				new Endpoint<int>(excludedPoint, EndpointLocation.Left), -1)
			.SetName("AndExcludedRightAndExcludedLeft_ReturnNegative");
		yield return new TestCaseData(
				new Endpoint<int>(excludedPoint, EndpointLocation.Left),
				new Endpoint<int>(includedPoint, EndpointLocation.Left), 1)
			.SetName("AndExcludedLeftAndIncludedLeft_ReturnPositive");
		yield return new TestCaseData(
				new Endpoint<int>(excludedPoint, EndpointLocation.Right),
				new Endpoint<int>(includedPoint, EndpointLocation.Right), -1)
			.SetName("AndExcludedRightAndIncludedRight_ReturnNegative");
		yield return new TestCaseData(
				new Endpoint<int>(excludedPoint, EndpointLocation.Left),
				new Endpoint<int>(includedPoint, EndpointLocation.Right), 1)
			.SetName("AndExcludedLeftAndIncludedRight_ReturnPositive");
		yield return new TestCaseData(
				new Endpoint<int>(excludedPoint, EndpointLocation.Right),
				new Endpoint<int>(includedPoint, EndpointLocation.Left), -1)
			.SetName("AndExcludedRightAndIncludedLeft_ReturnNegative");
	}
}