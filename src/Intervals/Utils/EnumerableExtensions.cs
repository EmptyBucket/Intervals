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
    public static IEnumerable<T> MergeAscending<T>(this IEnumerable<T> first, IEnumerable<T> second)
        where T : IComparable<T>
    {
        using var firstEnumerator = first.GetEnumerator();
        using var secondEnumerator = second.GetEnumerator();
        bool firstHas = firstEnumerator.MoveNext(), secondHas = secondEnumerator.MoveNext();

        if (firstHas && secondHas)
        {
            IEnumerator<T> currentEnumerator;

            do
            {
                currentEnumerator = firstEnumerator.Current.CompareTo(secondEnumerator.Current) <= 0
                    ? firstEnumerator
                    : secondEnumerator;
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