using System;
using System.Collections.Generic;

namespace PeriodNet.Intervals
{
	//фичи:
	//лучи
	//вектора
	//компарер по размеру
	//попробовать сделать внутренний endpoint enumerable, чтобы достичь n в контексте всех операций
	public interface IInterval<T> : IEquatable<IInterval<T>>, IComparable<IInterval<T>>, IEnumerable<IInterval<T>>
		where T : IComparable<T>, IEquatable<T>
	{
		IEndpoint<T> Left { get; }

		IEndpoint<T> Right { get; }

		IntervalType IntervalType { get; }
	}
}