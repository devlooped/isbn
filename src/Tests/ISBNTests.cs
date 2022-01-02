using Xunit;
using Xunit.Abstractions;

public record ISBNTests(ITestOutputHelper Output)
{
    [Theory]
    [InlineData("9781484746455")]
    [InlineData("9780920668887")]
    [InlineData("9780920668375")]
    [InlineData("9781554072033")]
    [InlineData("9781554072842")]
    [InlineData("9781484746691")]
    [InlineData("9780736431194")]
    [InlineData("9789504964964")]
    [InlineData("9789504933946")]
    [InlineData("9789504968665")]
    [InlineData("9780753557525")]
    public void ParseIsbn(string isbn)
    {
        if (ISBN.TryParse(isbn, out var parsed))
        {
            Output.WriteLine($"Publisher: {parsed.Publisher}, Article: {parsed.Article} ({parsed.Group}, {parsed.GroupName})");
        }
        else
        {
            Assert.True(false, "Failed to parse " + isbn);
        }
    }

    [Fact]
    public void ParseNullIsbnReturnsFalse() => Assert.False(ISBN.TryParse(null, out var _));
}
