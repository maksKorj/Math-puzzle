using System;

public static class NumberToTextConvetor
{
    public static string GetText(int number)
        => (number < 20) ? ConvertNumberToString(number) : ConvertHighTensToString(number);

    public static string ConvertNumberToString(int i)
    {
        return i switch
        {
            3 => "three",
            4 => "four",
            5 => "five",
            6 => "six",
            7 => "seven",
            8 => "eight",
            9 => "nine",
            10 => "ten",
            11 => "eleven",
            12 => "twelve",
            13 => "thirteen",
            14 => "fourteen",
            15 => "fiveteen",
            16 => "sixteen",
            17 => "seventeen",
            18 => "eighteen",
            19 => "nineteen",
            _ => throw new IndexOutOfRangeException(String.Format("{0} is incorrect!", i)),
        };
    }

    public static string ConvertHighTensToString(int n)
    {
        int tensDigit = (int)(Math.Floor(n / 10.0));

        string tensStr = tensDigit switch
        {
            2 => "twenty",
            3 => "thirty",
            4 => "forty",
            5 => "fifty",
            6 => "sixty",
            7 => "seventy",
            8 => "eighty",
            9 => "ninety",
            _ => throw new IndexOutOfRangeException(String.Format("{0} not in range 20-99", n)),
        };

        if (n % 10 == 0) return tensStr;

        string onesStr = ConvertNumberToString(n - tensDigit * 10);
        return tensStr + "-" + onesStr;
    }
}
