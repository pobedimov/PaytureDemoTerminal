using PaytureDemoTerminal.Operations;
using PaytureDemoTerminal.Responses;
using System.Xml;

// Отображение меню работы программы.
MenuDisplay();


static void MenuDisplay()
{
    Console.Clear();
    const string welcomeText = "Payture Terminal.\n\n\n\r";
    int centerX = (Console.WindowWidth / 2) - (welcomeText.Length / 2);
    Console.SetCursorPosition(centerX, 2);
    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write(welcomeText);
    Console.ResetColor();
    // Вывод меню консоли.
    Console.WriteLine("1) Запуск операции Pay с Api Payture с тестовыми параметрами из документации - \"1\"");
    Console.WriteLine("2) Запуск операции Pay с Api Payture c ручным вводом параметров - \"2\"");
    Console.WriteLine("3) Остановить работу и выйти - \"ESC\"\r\n");
}


// Выбор действия.
ConsoleKeyInfo key;
do
{
    key = Console.ReadKey();

    switch (key.Key)
    {
        // Операция Pay с тестовыми параметрами из документации.
        case ConsoleKey.D1:
            {          
                try
                {
                    Console.Clear();
                    IPayOperation payOperation = new PayOperationDemo();

                    payOperation.DisplayOperationInfo();
                    var responseString = await payOperation.PayAsync();

                    Console.WriteLine("\n\rОтвет от Payture Api\n\r");
                    Console.WriteLine($"{responseString}\r\n");
                    string parcedString = ResponseXmlParser.Parse(responseString);
                    Console.WriteLine(parcedString);
                }
                catch (Exception ex)
                {
                    if (ex is FormatException || ex is XmlException)
                    {
                        Console.WriteLine($"Ошибка! Получен некорретный формат(XML) ответа на операцию.\n\r {ex}");
                    }
                    if (ex is HttpRequestException)
                    {
                        Console.WriteLine($"HTTP-ответ завершился неудачей.\n\r {ex}");
                    }
                    if (ex is UriFormatException || ex is FormatException)
                    {
                        Console.WriteLine($"Не задан или верный формат Uri Api.\n\r {ex}");
                    }
                    throw;
                }

                Console.ReadKey();
                MenuDisplay();
                break;
            }
        // Операция Pay с ручным вводом параметров из консоли.
        case ConsoleKey.D2:
            {
                try
                {
                    Console.Clear();
                    IPayOperation payOperation = new PayOperationConsole();

                    payOperation.DisplayOperationInfo();
                    var responseString = await payOperation.PayAsync();

                    Console.WriteLine("\n\rОтвет от Payture Api\n\r");
                    Console.WriteLine($"{responseString}\r\n");
                    string parcedString = ResponseXmlParser.Parse(responseString);
                    Console.WriteLine(parcedString);
                }
                catch (Exception ex)
                {
                    if (ex is FormatException || ex is XmlException)
                    {
                        Console.WriteLine($"Ошибка! Получен некорретный формат(XML) ответа на операцию.\n\r {ex}");
                    }
                    if (ex is HttpRequestException)
                    {
                        Console.WriteLine($"HTTP-ответ завершился неудачей.\n\r {ex}");
                    }
                    if (ex is UriFormatException || ex is FormatException)
                    {
                        Console.WriteLine($"Не задан или верный формат Uri Api.\n\r {ex}");
                    }
                    throw;
                }

                Console.ReadKey();
                MenuDisplay();
                break;
            }
        default:
            break;
    }
}
while (key.Key != ConsoleKey.Escape); // по нажатию на Escape завершаем цикл
