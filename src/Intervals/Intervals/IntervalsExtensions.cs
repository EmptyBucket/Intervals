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

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
	public static bool IsOverlap<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
		where T : IComparable<T>, IEquatable<T> => left.Overlap(right).Any();

	public static bool IsInclude<T>(this IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right)
		where T : IComparable<T>, IEquatable<T>
	{
		var included = right.ToArray();
		return new HashSet<IInterval<T>>(included).SetEquals(left.Overlap(included));
	}

	private static int ToBalance<T>(this IEndpoint<T> endpoint) where T : IComparable<T>, IEquatable<T> =>
		(int)endpoint.Location * 2 - 1;

	private static IPoint<T> WithInvertedInclusion<T>(this IPoint<T> point) where T : IComparable<T>, IEquatable<T> =>
		Point.New(point.Value, point.Inclusion.Invert());

	private static IEnumerable<IEndpoint<T>> GetEndpoints<T>(this IInterval<T> interval)
		where T : IComparable<T>, IEquatable<T>
	{
		yield return interval.Left;
		yield return interval.Right;
	}
}