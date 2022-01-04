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

    [Theory]
    [InlineData("1590593618", "9781590593615")]
    [InlineData("978-1-492-07589-9", "9781492075899")]
    [InlineData("978 1 492 07589 9", "9781492075899")]
    [InlineData("1784162124", "9781784162122")]
    [InlineData("8860619645", "9788860619648")]
    public void NormalizeIsbn(string isbn, string normalized)
    {
        Assert.True(ISBN.TryParse(isbn, out var i));
        Assert.Equal(normalized, i!.ToString());
        Assert.Equal(normalized, i.CanonicalNumber);
    }
}
