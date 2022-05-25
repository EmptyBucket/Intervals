using Intervals.Points;

namespace Intervals.Intervals;

//фичи:
//лучи
//вектора
//попробовать сделать внутренний endpoint enumerable, чтобы достичь n в контексте всех операций
public interface IInterval<T> : IEquatable<IInterval<T>>, IComparable<IInterval<T>>, IEnumerable<IInterval<T>>
    where T : IComparable<T>, IEquatable<T>
{
    Endpoint<T> Left { get; }

    Endpoint<T> Right { get; }

    IntervalInclusion Inclusion { get; }
}