using FluentAssertions;
using Intervals.Utils;
using NUnit.Framework;

namespace Intervals.Tests.Utils;

public class BitHelperTests
{
    [Test]
    public void ToSign_WhenZero_ReturnOne()
    {
        const int bit = 0;

        var sign = BitHelper.ToSign(bit);

        sign.Should().Be(1);
    }

    [Test]
    public void ToSign_WhenPositiveOne_ReturnNegativeOne()
    {
        const int bit = 1;

        var sign = BitHelper.ToSign(bit);

        sign.Should().Be(-1);
    }
}