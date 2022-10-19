using System.Text.RegularExpressions;

Console.WriteLine("Let's go play! Please enter word to find a palindrome: ");

while (true)
{
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input))
    {
        Console.WriteLine("You didn't enter anything!");
        return;
    }

    try
    {
        Write($"For the word [{input}] the best palindrome is [{CreatePalindrome(input)}]", ConsoleColor.Red);
    }
    catch (Exception)
    {
        Console.WriteLine("Oops, something went wrong!");
    }
}

static string CreatePalindrome(string input)
{
    var first = CreatePalindromeForward(input);
    var second = CreatePalindromeBackward(input);

    if (first.Length == second.Length)
    {
        return string.CompareOrdinal(first, second) < 0 ? first : second;
    }

    return first.Length > second.Length ? second : first;
}

static string CreatePalindromeForward(string input)
{
    var palindrome = input;
    for (var i = input.Length - 2; i >= 0; i--)
    {
        palindrome += input[i];
    }

    return palindrome;
}

static string CreatePalindromeBackward(string input)
{
    var palindrome = input;
    var index = 1;
    while (!IsPalindrome(palindrome))
    {
        palindrome = Reverse(input)[..index] + input;
        index++;
    }

    return palindrome;
}

static string Reverse(string input)
{
    var charArray = input.ToCharArray();
    Array.Reverse(charArray);
    return new string(charArray);
}

static bool IsPalindrome(string input)
{
    return input.SequenceEqual(input.Reverse());
}

static void Write(string message, ConsoleColor color)
{
    var pieces = Regex.Split(message, @"(\[[^\]]*\])");

    foreach (var item in pieces)
    {
        var piece = item;

        if (piece.StartsWith("[") && piece.EndsWith("]"))
        {
            Console.ForegroundColor = color;
            piece = piece.Substring(1, piece.Length - 2);
        }

        Console.Write(piece);
        Console.ResetColor();
    }

    Console.WriteLine();
}