using System.Globalization;

namespace NumberToTextApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StreamWriter sw = new StreamWriter(@"D:\DownloadsD\Test.txt");
            decimal number = 125.03m;
            sw.WriteLine(ConvertDecimalToText(number));

            decimal number1 = 1234567890.45m;
            sw.WriteLine(ConvertDecimalToText(number1)); // it should print "один миллиард двести тридцать четыре миллиона пятьсот шестьдесят семь тысяч восемьсот девяносто и 45 коп."

            decimal number2 = 1000.75m;
            sw.WriteLine(ConvertDecimalToText(number2)); // it should print "одна тысяча и 75 коп."

            decimal number3 = 2500000.01m;
            sw.WriteLine(ConvertDecimalToText(number3)); // it should print "два миллиона пятьсот тысяч и 01 коп."
            sw.Close();
        }

        #region ConvertDecimalToText

        /// <summary>
        /// Конвертирует десятичное число в текстовое представление на русском языке.
        /// Часть после десятичной точки обрабатывается как копейки.
        /// </summary>
        /// <param name="number">Число для конвертации.</param>
        /// <returns>Текстовое представление числа.</returns>
        public static string ConvertDecimalToText(decimal number)
        {
            if (number == 0)
                return "ноль";

            string result = "";

            // обработка целой части числа
            int intPart = (int)number;
            result += ConvertIntegerToText(intPart);

            // обработка десятичной части числа
            decimal fractionalPart = number - intPart;
            if (fractionalPart > 0)
            {
                string fractionalPartString = fractionalPart.ToString(CultureInfo.InvariantCulture).Substring(2);
                result += fractionalPartString + " коп.";
            }

            return result.TrimStart();
        }

        #endregion ConvertDecimalToText

        #region ConvertIntegerToText

        /// <summary>
        /// Конвертирует целое число в текстовое представление на русском языке.
        /// </summary>
        /// <param name="number">Целое число для конвертации.</param>
        /// <returns>Текстовое представление числа.</returns>
        private static string ConvertIntegerToText(int number)
        {
            int[] array_int = new int[4];
            string[,] array_string = new string[4, 3] {
            { " миллиард", " миллиарда", " миллиардов" },
            { " миллион", " миллиона", " миллионов" },
            { " тысяча", " тысячи", " тысяч" },
            { "", "", "" }
        };

            array_int[0] = (number - (number % 1000000000)) / 1000000000;
            array_int[1] = ((number % 1000000000) - (number % 1000000)) / 1000000;
            array_int[2] = ((number % 1000000) - (number % 1000)) / 1000;
            array_int[3] = number % 1000;

            string result = "";

            if (number == 0) result = "ноль";
            else for (int i = 0; i < 4; i++)
                {
                    if (array_int[i] != 0)
                    {
                        if (((array_int[i] - (array_int[i] % 100)) / 100) != 0)
                            switch (((array_int[i] - (array_int[i] % 100)) / 100))
                            {
                                case 1: result += " сто"; break;
                                case 2: result += " двести"; break;
                                case 3: result += " триста"; break;
                                case 4: result += " четыреста"; break;
                                case 5: result += " пятьсот"; break;
                                case 6: result += " шестьсот"; break;
                                case 7: result += " семьсот"; break;
                                case 8: result += " восемьсот"; break;
                                case 9: result += " девятьсот"; break;
                            }
                        if (((array_int[i] % 100) - ((array_int[i] % 100) % 10)) / 10 != 1)
                        {
                            switch (((array_int[i] % 100) - ((array_int[i] % 100) % 10)) / 10)
                            {
                                case 2: result += " двадцать"; break;
                                case 3: result += " тридцать"; break;
                                case 4: result += " сорок"; break;
                                case 5: result += " пятьдесят"; break;
                                case 6: result += " шестьдесят"; break;
                                case 7: result += " семьдесят"; break;
                                case 8: result += " восемьдесят"; break;
                                case 9: result += " девяносто"; break;
                            }
                            switch (array_int[i] % 10)
                            {
                                case 1: if (i == 2) result += " одна"; else result += " один"; break;
                                case 2: if (i == 2) result += " две"; else result += " два"; break;
                                case 3: result += " три"; break;
                                case 4: result += " четыре"; break;
                                case 5: result += " пять"; break;
                                case 6: result += " шесть"; break;
                                case 7: result += " семь"; break;
                                case 8: result += " восемь"; break;
                                case 9: result += " девять"; break;
                            }
                        }
                        else switch (array_int[i] % 100)
                            {
                                case 10: result += " десять"; break;
                                case 11: result += " одиннадцать"; break;
                                case 12: result += " двенадцать"; break;
                                case 13: result += " тринадцать"; break;
                                case 14: result += " четырнадцать"; break;
                                case 15: result += " пятнадцать"; break;
                                case 16: result += " шестнадцать"; break;
                                case 17: result += " семнадцать"; break;
                                case 18: result += " восемнадцать"; break;
                                case 19: result += " девятнадцать"; break;
                            }
                        if (array_int[i] % 100 >= 10 && array_int[i] % 100 <= 19) result += array_string[i, 2];
                        else switch (array_int[i] % 10)
                            {
                                case 1: result += array_string[i, 0]; break;
                                case 2:
                                case 3:
                                case 4: result += array_string[i, 1]; break;
                                case 5:
                                case 6:
                                case 7:
                                case 8:
                                case 9:
                                case 0: result += array_string[i, 2]; break;
                            }
                    }
                }

            return $"{result.TrimStart()} руб., ";
        }

        #endregion ConvertIntegerToText
    }
}