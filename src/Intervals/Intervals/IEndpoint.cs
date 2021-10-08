using System;

namespace PeriodNet.Intervals
{
	public interface IEndpoint<T> : IPoint<T>, IComparable<IEndpoint<T>>, IEquatable<IEndpoint<T>>
		where T : IComparable<T>, IEquatable<T>
	{
		EndpointLocation Location { get; }
	}
}