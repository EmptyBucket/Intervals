// ReSharper disable ShiftExpressionZeroLeftOperand

using System;

namespace PeriodNet.Intervals
{
	[Flags]
	public enum IntervalType
	{
		Open = Inclusion.Excluded << EndpointLocation.Left | Inclusion.Excluded << EndpointLocation.Right,
		LeftOpen = Inclusion.Excluded << EndpointLocation.Left | Inclusion.Included << EndpointLocation.Right,
		RightOpen = Inclusion.Included << EndpointLocation.Left | Inclusion.Excluded << EndpointLocation.Right,
		Closed = Inclusion.Included << EndpointLocation.Left | Inclusion.Included << EndpointLocation.Right
	}
}