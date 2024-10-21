using System.Numerics;
using Xunit;
using NumbersToWords;

public class NumberToWordsConverterTests
{
    private readonly NumberToWordsConverter converter;

    public NumberToWordsConverterTests()
    {
        converter = new NumberToWordsConverter();
    }

    [Fact]
    public void ConvertToWords_Zero()
    {

        BigInteger input = 0;
        string expected = "Zero";
        string result = converter.ConvertToWords(input);
        Assert.Equal(expected, result);
    }
    [Fact]
    public void ConvertToWords_Integers()
    {

        BigInteger input = 5;
        string expected = "Five";
        string result = converter.ConvertToWords(input);
        Assert.Equal(expected, result);
    }
    [Fact]
    public void ConvertToWords_Hundred()
    {
        decimal input = 100;
        string expected = "One Hundred";
        string result = converter.ConvertToWords(input);
        Assert.Equal(expected, result);
    }
    [Fact]
    public void ConvertToWords_NegativeValue()
    {

        BigInteger input = -1;
        string expected = "Negative One";
        string result = converter.ConvertToWords(input);
        Assert.Equal(expected, result);
    }
    [Fact]
    public void ConvertToWords_TensOfThousands()
    {
        BigInteger input = 54321;
        string expected = "Fifty Four Thousand Three Hundred and Twenty One";
        string result = converter.ConvertToWords(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void ConvertToWords_TensOfMillions()
    {
        BigInteger input = 54321123;
        string expected = "Fifty Four Million Three Hundred Twenty One Thousand and One Hundred and Twenty Three";
        string result = converter.ConvertToWords(input);
        Assert.Equal(expected, result);
    }
    [Fact]
    public void ConvertToWords_Billions()
    {
        BigInteger input = 5432112323123123;
        string expected = "Five Million Four Hundred Thirty Two Thousand and One Hundred and Twelve Billion Three Hundred Twenty Three Million and One Hundred Twenty Three Thousand and One Hundred and Twenty Three";
        string result = converter.ConvertToWords(input);
        Assert.Equal(expected, result);
    }
}
