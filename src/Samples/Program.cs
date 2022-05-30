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

﻿using Intervals.GranularIntervals;
using Intervals.Intervals;

var enumerable = new[]
    {
        new Interval<int>(0, 10, IntervalInclusion.LeftOpened),
        new Interval<int>(20, 30, IntervalInclusion.Closed),
        new Interval<int>(40, 50, IntervalInclusion.RightOpened)
    }
    .Combine(new[]
        {
            new Interval<int>(10, 20, IntervalInclusion.Opened),
            new Interval<int>(30, 40, IntervalInclusion.Opened)
        }
        .Subtract(new[]
        {
            new Interval<int>(15, 25, IntervalInclusion.Closed),
            new Interval<int>(25, 35, IntervalInclusion.Closed)
        }))
    .Overlap(new[]
        {
            new Interval<int>(15, 20, IntervalInclusion.Closed),
            new Interval<int>(30, 35, IntervalInclusion.Closed)
        }
        .SymmetricDifference(new Interval<int>(20, 35, IntervalInclusion.Opened)))
    .Overlap(new Interval<int>(20, 25, IntervalInclusion.Opened))
    .ToArray();

var result = new QuarterInterval(2022, 3)
    .Move(-1)
    .Combine(new MonthInterval(2022, 1).Move(1))
    .SelectMany(i => i.SplitByMonths(2));

Console.WriteLine();