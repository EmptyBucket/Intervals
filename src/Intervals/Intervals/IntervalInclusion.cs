// ReSharper disable ShiftExpressionZeroLeftOperand

using Intervals.Points;

namespace Intervals.Intervals;

[Flags]
public enum IntervalInclusion
{
    Opened = Inclusion.Excluded << EndpointLocation.Left | Inclusion.Excluded << EndpointLocation.Right,
    LeftOpened = Inclusion.Excluded << EndpointLocation.Left | Inclusion.Included << EndpointLocation.Right,
    RightOpened = Inclusion.Included << EndpointLocation.Left | Inclusion.Excluded << EndpointLocation.Right,
    Closed = Inclusion.Included << EndpointLocation.Left | Inclusion.Included << EndpointLocation.Right
}