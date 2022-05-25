namespace Intervals.GranularIntervals;

public interface IGranularInterval<T, out TInterval>
    where T : IComparable<T>, IEquatable<T>
    where TInterval : IGranularInterval<T, TInterval>
{
    TInterval GetPrev();

    TInterval GetNext();
}