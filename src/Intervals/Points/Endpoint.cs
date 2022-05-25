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

using Intervals.Utils;

namespace Intervals.Points;

public readonly record struct Endpoint<T> : IComparable<Endpoint<T>> where T : IComparable<T>, IEquatable<T>
{
    private readonly Point<T> _point;

    public Endpoint(Point<T> point, EndpointLocation location)
    {
        _point = point;
        Location = location;
    }

    public T Value => _point.Value;

    public Inclusion Inclusion => _point.Inclusion;

    public EndpointLocation Location { get; }

    public int CompareTo(Endpoint<T> other)
    {
        var valueCompared = Value.CompareTo(other.Value);

        if (valueCompared != 0) return valueCompared;

        var locationCompared = Location - other.Location;
        var bothIsIncluded = Inclusion & other.Inclusion;

        // left location more right location, except both locations included
        //   )<(   ]<(   )<[   ]>[
        if (locationCompared != 0) return locationCompared * BitHelper.ToSign((int)bothIsIncluded);

        var inclusionCompared = Inclusion - other.Inclusion;
        var thisIsLeft = Location;

        // same inclusions equals, exclude more include when left location, otherwise include more exclude
        //   )=)   (=(   ]=]   [=[   )<]   (>[
        return inclusionCompared * BitHelper.ToSign((int)thisIsLeft);
    }

    public override string ToString() => Location switch
    {
        EndpointLocation.Left => Inclusion switch
        {
            Inclusion.Excluded => $"({Value}",
            Inclusion.Included => $"[{Value}",
            _ => throw new ArgumentOutOfRangeException()
        },
        EndpointLocation.Right => Inclusion switch
        {
            Inclusion.Excluded => $"{Value})",
            Inclusion.Included => $"{Value}]",
            _ => throw new ArgumentOutOfRangeException()
        },
        _ => throw new ArgumentOutOfRangeException()
    };

    public static implicit operator Point<T>(Endpoint<T> endpoint) => endpoint._point;
}