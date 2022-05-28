using Intervals.GranularIntervals;using Intervals.Intervals;

var enumerable =
    new[]
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

var nextQuarter = new QuarterInterval(2022, 3).GetNext();

Console.WriteLine();