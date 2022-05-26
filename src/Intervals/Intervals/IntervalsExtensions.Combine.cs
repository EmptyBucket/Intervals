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

using Intervals.Points;

namespace Intervals.Intervals;

public static partial class IntervalsExtensions
{
    public static IEnumerable<IInterval<T>> Combine<T>(this IEnumerable<IInterval<T>> left,
        IEnumerable<IInterval<T>> right) where T : IComparable<T>, IEquatable<T> =>
        new CombineEnumerable<T>(left, right);

    private class CombineEnumerable<T> : IntervalDeviationEnumerable<T> where T : IEquatable<T>, IComparable<T>
    {
        public CombineEnumerable(IEnumerable<IInterval<T>> left, IEnumerable<IInterval<T>> right) : base(left, right)
        {
        }

        protected override bool GetDeviance(IReadOnlyList<int> batchBalances) => batchBalances.Any(b => b > 0);

        protected override Point<T> CreatePoint(Point<T> point, int batchIndex) => point;
    }
}