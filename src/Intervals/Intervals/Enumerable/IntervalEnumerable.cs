using System.Collections;

namespace Intervals.Intervals.Enumerable;

internal class IntervalEnumerable<T> : IEnumerable<Interval<T>> where T : IComparable<T>, IEquatable<T>
{
    private readonly Interval<T> _interval;

    public IntervalEnumerable(Interval<T> interval)
    {
        _interval = interval;
    }

    public IEnumerator<Interval<T>> GetEnumerator()
    {
        yield return _interval;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}