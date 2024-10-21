using System.Numerics;

namespace NumbersToWords
{
    public class NumberToWordsConverter
    {
        private static string number_zero = "Zero";
        private static string[] number_ones = { "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten" };
        private static string[] number_teens = { "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        private static string[] number_tens = { "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

        public string ConvertToWords(decimal number)
        {
            string result = "";
            if (number == 0)
            {
                return number_zero;
            }
            if (number < 0)
            {
                result += "Negative ";
                number = Math.Abs(number);
            }
            //Partition in whole and decimal numbers
            long wholePart = (long)Math.Floor(number);
            int decimalPart = (int)((number - wholePart) * 100);

            // Convert the whole part to words
            if (wholePart > 0)
            {
                result += ConvertNumber(new BigInteger(wholePart));
            }
            else
            {
                result += number_zero;
            }

            // Convert the decimal part
            if (decimalPart > 0)
            {
                result += $" and {ConvertNumber(new BigInteger(decimalPart))} Cents";
            }
            return result;
        }

        // Handle the larger numbers
        public string ConvertToWords(BigInteger number)
        {
            string result = "";
            if (number == 0)
            {
                return number_zero;
            }
            if (number < 0)
            {
                result += "Negative ";
                number = BigInteger.Abs(number);
            }
            result += ConvertNumber(number);
            return result;
        }

        public string ConvertNumber(BigInteger number)
        {
            var result = new List<string>();
            var largeUnits = new Dictionary<BigInteger, string> {
                { new BigInteger(1000000000), "Billion" },
                { new BigInteger(1000000), "Million" },
                { new BigInteger(1000), "Thousand" },
                { new BigInteger(100), "Hundred" }
            };
            bool isFirst = true;

            // Handle larger units
            foreach (var unit in largeUnits)
            {
                if (number >= unit.Key)
                {
                    // After the first number, add and to the string
                    string and = "";
                    if (!isFirst)
                    {
                        and = " and";
                    }
                    BigInteger amount = number / unit.Key;
                    result.Add($"{ConvertNumber(amount)} {unit.Value}{and}");
                    number %= unit.Key;
                    isFirst = false;
                }
            }

            // Handle numbers from 20 to 99
            if (number >= 20)
            {
                int tens = (int)(number / 10);
                result.Add(number_tens[tens - 2]);
                number %= 10;
            }
            else if (number >= 11 && number <= 19)
            {
                result.Add(number_teens[(int)number - 11]);
                number = 0;
            }

            // Handle numbers from 1 to 9
            if (number > 0)
            {
                result.Add(number_ones[(int)number - 1]);
            }

            return string.Join(" ", result);
        }
    }
}
