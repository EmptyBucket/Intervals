using FluentAssertions;
using Intervals.GranularIntervals;
using Newtonsoft.Json;
using NUnit.Framework;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Intervals.Tests.GranularIntervals;

public class PartOfMonthGranularIntervalTests
{
    [Test]
    public void Serialize_WhenSystemTextJson_ShouldNotThrowException()
    {
        var interval = new PartOfMonthGranularInterval(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1), 3);

        var action = new Action(() => JsonSerializer.Serialize(interval));

        action.Should().NotThrow();
    }

    [Test]
    public void Deserialize_WhenSystemTextJson_ShouldNotThrowException()
    {
        const string str =
            "{\"LeftValue\":\"2022-01-01T00:00:00\",\"RightValue\":\"2023-01-01T00:00:00\",\"GranulesCount\":1,\"Inclusion\":2}";

        var action = new Action(() => JsonSerializer.Deserialize<PartOfMonthGranularInterval>(str));

        action.Should().NotThrow();
    }

    [Test]
    public void Serialize_WhenNewtonsoftJson_ShouldNotThrowException()
    {
        var interval = new PartOfMonthGranularInterval(new DateTime(2022, 1, 1), new DateTime(2023, 1, 1), 3);

        var action = new Action(() => JsonConvert.SerializeObject(interval));

        action.Should().NotThrow();
    }

    [Test]
    public void Deserialize_WhenNewtonsoftJson_ShouldNotThrowException()
    {
        const string str =
            "{\"LeftValue\":\"2022-01-01T00:00:00\",\"RightValue\":\"2023-01-01T00:00:00\",\"GranulesCount\":1,\"Inclusion\":2}";

        var action = new Action(() => JsonConvert.DeserializeObject<PartOfMonthGranularInterval>(str));

        action.Should().NotThrow();
    }
}