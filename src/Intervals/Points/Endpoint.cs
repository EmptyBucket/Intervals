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

public static class Endpoint
{
    /// <summary>
    /// Returns endpoint with specified <paramref name="value" />, <paramref name="inclusion" /> and left location
    /// </summary>
    /// <param name="value"></param>
    /// <param name="inclusion"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Endpoint<T> Left<T>(T value, Inclusion inclusion) where T : IEquatable<T>, IComparable<T> =>
        new(value, inclusion, EndpointLocation.Left);

    /// <summary>
    /// Returns endpoint with specified <paramref name="point" /> and left location
    /// </summary>
    /// <param name="point"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Endpoint<T> Left<T>(Point<T> point) where T : IEquatable<T>, IComparable<T> =>
        new(point, EndpointLocation.Left);

    /// <summary>
    /// Returns endpoint with specified <paramref name="value" />, <paramref name="inclusion" /> and right location
    /// </summary>
    /// <param name="value"></param>
    /// <param name="inclusion"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Endpoint<T> Right<T>(T value, Inclusion inclusion) where T : IEquatable<T>, IComparable<T> =>
        new(value, inclusion, EndpointLocation.Right);
    
    /// <summary>
    /// Returns endpoint with specified <paramref name="point" /> and right location
    /// </summary>
    /// <param name="point"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Endpoint<T> Right<T>(Point<T> point) where T : IEquatable<T>, IComparable<T> =>
        new(point, EndpointLocation.Right);
}

/// <summary>
/// Represents an endpoint instance
/// </summary>
/// <param name="Value"></param>
/// <param name="Inclusion"></param>
/// <param name="Location"></param>
/// <typeparam name="T"></typeparam>
public readonly record struct Endpoint<T>(T Value, Inclusion Inclusion, EndpointLocation Location)
    : IComparable<Endpoint<T>> where T : IComparable<T>, IEquatable<T>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Intervals.Points.Endpoint"/>
    /// with specified <paramref name="point" /> and <paramref name="location" />
    /// </summary>
    /// <param name="point"></param>
    /// <param name="location"></param>
    public Endpoint(Point<T> point, EndpointLocation location) : this(point.Value, point.Inclusion, location)
    {
    }

    /// <summary>
    /// Deconstructs instance into the specified <paramref name="point" /> and <paramref name="location" />
    /// </summary>
    /// <param name="point"></param>
    /// <param name="location"></param>
    public void Deconstruct(out Point<T> point, out EndpointLocation location) => (point, location) = (this, Location);

    /// <summary>
    /// Compares this instance to a specified <paramref name="other" /> and returns an indication of their relative values
    /// First, instances are compared according to their values, then according to the rule:
    /// ) less than (, ] less than(, ) less thank [, ] less than [, ) equal ), ( equal (, ] equal ], [ equal [, ) less than ], ( greater than [
    /// </summary>
    /// <param name="other"></param>
    /// <returns>
    /// <list type="table">
    /// <listheader><term> Return Value</term><description> Description</description></listheader>
    /// <item><term> Less than zero</term><description> This instance is less than <paramref name="other" />.</description></item>
    /// <item><term> Zero</term><description> This instance is equal to <paramref name="other" />.</description></item>
    /// <item><term> Greater than zero</term><description> This instance is greater than <paramref name="other" />.</description></item>
    /// </list>
    /// </returns>
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

    public static implicit operator Point<T>(Endpoint<T> endpoint) => new(endpoint.Value, endpoint.Inclusion);

    /// <summary>
    /// Converts the value of this instance to "{[(,),[,]]}{Value}{[(,),[,]]}" format
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
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
}