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

namespace Intervals.Utils;

internal static class EnumerableExtensions
{
    /// <summary>
    /// Returns an ordered sequence of the specified ordered <paramref name="left" /> and ordered <paramref name="right" /> intervals
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="selector"></param>
    /// <param name="ascending"></param>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public static IEnumerable<TSource> Merge<TSource, TKey>(this IEnumerable<TSource> left,
        IEnumerable<TSource> right, Func<TSource, TKey> selector, bool ascending = true)
        where TKey : IComparable<TKey>
    {
        Func<int, bool> order = ascending ? i => i <= 0 : i => i >= 0;
        using var firstEnumerator = left.GetEnumerator();
        using var secondEnumerator = right.GetEnumerator();
        bool firstHas = firstEnumerator.MoveNext(), secondHas = secondEnumerator.MoveNext();

        if (firstHas && secondHas)
        {
            IEnumerator<TSource> currentEnumerator;

            do
            {
                var firstKey = selector(firstEnumerator.Current);
                var secondKey = selector(secondEnumerator.Current);
                currentEnumerator = order(firstKey.CompareTo(secondKey)) ? firstEnumerator : secondEnumerator;
                yield return currentEnumerator.Current;
            } while (currentEnumerator.MoveNext());

            currentEnumerator = currentEnumerator == firstEnumerator ? secondEnumerator : firstEnumerator;

            do yield return currentEnumerator.Current;
            while (currentEnumerator.MoveNext());
        }
        else if (firstHas)
        {
            do yield return firstEnumerator.Current;
            while (firstEnumerator.MoveNext());
        }
        else if (secondHas)
        {
            do yield return secondEnumerator.Current;
            while (secondEnumerator.MoveNext());
        }
    }
}