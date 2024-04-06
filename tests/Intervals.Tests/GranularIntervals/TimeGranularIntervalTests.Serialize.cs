using FluentAssertions;
using Intervals.GranularIntervals;
using Newtonsoft.Json;
using NUnit.Framework;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Intervals.Tests.GranularIntervals;

public partial class TimeGranularIntervalTests
{
    [Test]
    public void Serialize_WhenSystemTextJson_ShouldNotThrowException()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 2);
        var interval = new TimeGranularInterval(leftValue, rightValue, rightValue - leftValue);

        var action = new Action(() => JsonSerializer.Serialize(interval));

        action.Should().NotThrow();
    }

    [Test]
    public void Deserialize_WhenSystemTextJson_ShouldNotThrowException()
    {
        const string str =
            """{"LeftValue":"2022-01-01T00:00:00","RightValue":"2022-01-02T00:00:00","GranuleLength":"1.00:00:00","Inclusion":2}""";

        var action = new Action(() => JsonSerializer.Deserialize<TimeGranularInterval>(str));

        action.Should().NotThrow();
    }

    [Test]
    public void Serialize_WhenNewtonsoftJson_ShouldNotThrowException()
    {
        var leftValue = new DateTime(2022, 1, 1);
        var rightValue = new DateTime(2022, 1, 2);
        var interval = new TimeGranularInterval(leftValue, rightValue, rightValue - leftValue);

        var action = new Action(() => JsonConvert.SerializeObject(interval));

        action.Should().NotThrow();
    }

    [Test]
    public void Deserialize_WhenNewtonsoftJson_ShouldNotThrowException()
    {
        const string str =
            """{"LeftValue":"2022-01-01T00:00:00","RightValue":"2022-01-02T00:00:00","GranuleLength":"1.00:00:00","Inclusion":2}""";

        var action = new Action(() => JsonConvert.DeserializeObject<TimeGranularInterval>(str));

        action.Should().NotThrow();
    }
}