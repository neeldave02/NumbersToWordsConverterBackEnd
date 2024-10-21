using Microsoft.AspNetCore.Mvc;
using NumbersToWords.Controllers;
using Xunit;

namespace NumbersToWords.Tests
{
    public class NumbersToWordsControllerTests
    {
        private readonly NumbersToWordsController controller;

        public NumbersToWordsControllerTests()
        {
            controller = new NumbersToWordsController();
        }
        [Fact]
        public void ConvertToWords_Zero()
        {
            string number = "0";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Zero", result.Content);
        }
        [Fact]
        public void ConvertToWords_Integers()
        {
            string number = "6";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Six", result.Content);
        }
        [Fact]
        public void ConvertToWords_Hundred()
        {
            string number = "100";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("One Hundred", result.Content);
        }

        [Fact]
        public void ConvertToWords_Negative()
        {
            string number = "-1";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Negative One", result.Content);
        }

        [Fact]
        public void ConvertToWords_TensOfThousands()
        {
            string number = "54321";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Fifty Four Thousand Three Hundred and Twenty One", result.Content);
        }

        [Fact]
        public void ConvertToWords_TensOfMillions()
        {
            string number = "54321123";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Fifty Four Million Three Hundred Twenty One Thousand and One Hundred and Twenty Three", result.Content);
        }
        [Fact]
        public void ConvertToWords_Billions()
        {
            string number = "5432112323123123";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Five Million Four Hundred Thirty Two Thousand and One Hundred and Twelve Billion Three Hundred Twenty Three Million and One Hundred Twenty Three Thousand and One Hundred and Twenty Three", result.Content);
        }
        [Fact]
        public void ConvertToWords_LargeWithDecimal()
        {
            string number = "12312312312312312223.45";
            var result = controller.ConvertNumberToWords(number) as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
        [Fact]
        public void ConvertToWords_NonNumber()
        {
            string number = "abcde";
            var result = controller.ConvertNumberToWords(number) as BadRequestObjectResult;
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("The input number is invalid.", result.Value);
        }
        [Fact]
        public void ConvertToWords_DecimalOnly()
        {
            string number = "0.45";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Zero and Forty Five Cents", result.Content);
        }
        [Fact]
        public void ConvertToWords_NumberWithZeroDecimals()
        {
            string number = "123.00";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("One Hundred Twenty Three", result.Content);
        }
        [Fact]
        public void ConvertToWords_LargeNegative()
        {
            string number = "-123123123123123";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Negative One Hundred Twenty Three Thousand One Hundred and Twenty Three Billion One Hundred Twenty Three Million and One Hundred Twenty Three Thousand and One Hundred and Twenty Three", result.Content);
        }
        [Fact]
        public void ConvertToWords_ZeroWithDecimals()
        {
            string number = "0.00";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Zero", result.Content);
        }
        [Fact]
        public void ConvertToWords_VerySmallValue()
        {
            string number = "0.000001";
            var result = controller.ConvertNumberToWords(number) as ContentResult;
            Assert.NotNull(result);
            Assert.Equal("text/plain", result.ContentType);
            Assert.Equal("Zero", result.Content);
        }
    }
}
