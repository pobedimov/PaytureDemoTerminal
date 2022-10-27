using System.Text;

namespace PaytureDemoTerminal.Extentions;

/// <summary>
/// Представляет класс для работы с вводом в консоль.
/// </summary>
public static class ConsoleOperator
{
    /// <summary>
    /// Осуществляет ввод цифр в консоле с ограничением по количеству.
    /// </summary>
    /// <param name="maxCharCount">Максимальное количество вводимых символов.</param>
    /// <returns>Результирующая строка.</returns>
    public static string InputDigitDataConsole(int maxCharCount)
    {
        var sb = new StringBuilder();
        while (true)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    return sb.ToString();
                case ConsoleKey.Backspace:
                    if (sb.Length > 0)
                    {
                        sb.Length -= 1;
                        Console.Write("\b \b");
                    }
                    break;
                default:
                    if (char.IsDigit(key.KeyChar) && sb.Length < maxCharCount)
                    {
                        sb.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// Осуществляет ввод букв и пробелов в консоль с ограничением по количеству.
    /// </summary>
    /// <param name="maxCharCount">Максимальное количество вводимых символов.</param>
    /// <returns>Результирующая строка.</returns>
    public static string InputStringDataConsole(int maxCharCount)
    {
        var sb = new StringBuilder();
        while (true)
        {
            var key = Console.ReadKey(true);
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Console.WriteLine();
                    return sb.ToString();
                case ConsoleKey.Backspace:
                    if (sb.Length > 0)
                    {
                        sb.Length -= 1;
                        Console.Write("\b \b");
                    }
                    break;
                default:
                    if ((char.IsLetter(key.KeyChar) || char.IsWhiteSpace(key.KeyChar)) && sb.Length < maxCharCount)
                    {
                        sb.Append(key.KeyChar);
                        Console.Write(key.KeyChar);
                    }
                    break;
            }
        }
    }
}
