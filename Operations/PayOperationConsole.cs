using PaytureDemoTerminal.Extentions;
using PaytureDemoTerminal.Requests;
using System.Net.Http.Headers;
using System.Text;

namespace PaytureDemoTerminal.Operations;

/// <summary>
/// Представляет класс для создания платежа через Api Pay с вводом данных из консоли.
/// </summary>
public sealed class PayOperationConsole : IPayOperation
{
    /// <summary>
    /// Представляет адрес Api для выполнения операции Pay.
    /// </summary>
    public Uri ApiUri { get; private set; }

    /// <summary>
    /// Представляет запрос для операции "Pay" Api Payture.
    /// </summary>
    private RequestPay RequestPay { get; set; }

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public PayOperationConsole()
    {
        ApiSettings.ApiDomain = "payture.com";
        ApiSettings.ApiEnvironment = "sandbox3";
        ApiSettings.ApiInterface = "api";
        ApiSettings.ApiCommand = "Pay";

        ApiUri = ApiSettings.GetFullApiAddress();

        RequestPay = new RequestPay();
    }

    
    /// <summary>
    /// Отображение запроса.
    /// </summary>
    public void DisplayOperationInfo()
    {
        // Формирование данных для запроса.
        MakingInputData();

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Key={RequestPay.Key}\r");
        Console.WriteLine($"Amount={RequestPay.Amount}\r");
        Console.WriteLine($"OrderId={RequestPay.OrderId}\r");
        Console.WriteLine(RequestPay.PayInfo);
        Console.ResetColor();
    }

    /// <summary>
    /// Отправляет запрос на выполнение операции Pay.
    /// </summary>
    /// <exception cref="HttpRequestException">HTTP-ответ завершился неудачей.</exception>
    /// <exception cref="UriFormatException">Ошибка формата Uri.</exception>
    /// <exception cref="FormatException">Некорректно задан формат Uri.</exception>
    /// <returns>Ответ от Api на операцию Pay.</returns>
    public async Task<string> PayAsync()
    {
        HttpContent httpContent = new StringContent(RequestPay.GetUriPayRequest(), Encoding.UTF8, "application/x-www-form-urlencoded");

        return await Operation.ExecuteAsync(ApiUri, httpContent, new MediaTypeWithQualityHeaderValue("application/xml"));
    }

    /// <summary>
    /// Осуществляет ввод с консоли данных для формирования данных запроса операции.
    /// </summary>
    public void MakingInputData()
    {
        Console.WriteLine("Формирование данных для осуществелниея операции платежа - Pay\n\r");

        try
        {
            Console.WriteLine("Введите данные программной среды - {Environment} по умолчанию - \"sandbox3\"\r");
            string? apiEnvironment = Console.ReadLine();
            ApiSettings.ApiEnvironment = !string.IsNullOrEmpty(apiEnvironment)
                ? ApiSettings.ApiEnvironment = apiEnvironment
                : ApiSettings.ApiEnvironment = "sandbox3";

            Console.WriteLine("Введите именование домена, расположения Api - {ApiDomain} по умолчанию - \"payture.com\"\r");
            string? apiDomain = Console.ReadLine();
            ApiSettings.ApiDomain = !string.IsNullOrEmpty(apiDomain)
                ? ApiSettings.ApiDomain = apiDomain
                : ApiSettings.ApiDomain = "payture.com";

            Console.WriteLine("Введите используемый программный интерфейс - {api} по умолчанию - \"api\"\r");
            string? apiInterface = Console.ReadLine();
            ApiSettings.ApiInterface = !string.IsNullOrEmpty(apiInterface)
                ? ApiSettings.ApiInterface = apiInterface
                : ApiSettings.ApiInterface = "api";

            Console.WriteLine("Введите название выполняемой команды -{Command} по умолчанию - \"Pay\"\r");
            string? apiCommand = Console.ReadLine();
            ApiSettings.ApiCommand = !string.IsNullOrEmpty(apiCommand)
                ? ApiSettings.ApiCommand = apiCommand
                : ApiSettings.ApiCommand = "Pay";

            // Вывод сформированного результата Uri на экран.
            try
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                ApiUri = ApiSettings.GetFullApiAddress();
                Console.WriteLine(ApiUri.ToString());
                Console.ResetColor();
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Ошибка формата Uri для Api {ex}");
            }

            Console.WriteLine("\rНаименование платежного Терминала по умолчанию - \"Merchant\"\r");
            string? terminalName = Console.ReadLine();
            RequestPay.Key = !string.IsNullOrEmpty(terminalName)
                ? RequestPay.Key = terminalName
                : RequestPay.Key = "Merchant";


            Console.WriteLine("Сумма платежа в копейках по умолчанию - 0\r");
            string? amount = ConsoleOperator.InputDigitDataConsole(int.MaxValue);
            RequestPay.Amount = !string.IsNullOrEmpty(amount)
                ? RequestPay.Amount = amount
                : RequestPay.Amount = "0";

            string orderId = Guid.NewGuid().ToString();
            Console.WriteLine($"\rУникальный идентификатор платежа в системе Продавца - {orderId}\r");
            RequestPay.OrderId = orderId;

            // Формирование информации о платеже.
            RequestPayInfo payInfo = new();
            payInfo.OrderId = orderId;
            if (!string.IsNullOrEmpty(amount))
                payInfo.Amount = amount;
            else
                payInfo.Amount = "0";

            Console.WriteLine("Номер карты\r");
            string? pan = ConsoleOperator.InputDigitDataConsole(19);
            payInfo.PAN = !string.IsNullOrEmpty(pan)
                ? payInfo.PAN = pan
                : payInfo.PAN = "0000000000000";

            Console.WriteLine("Месяц истечения срока действия карты\r");
            string? eMonth = ConsoleOperator.InputDigitDataConsole(2);
            if (!string.IsNullOrEmpty(eMonth) && int.Parse(eMonth) <= 12)
                payInfo.EMonth = eMonth;
            else
                payInfo.EMonth = "00";

            Console.WriteLine("Год истечения срока действия карты\r");
            string? eYear = ConsoleOperator.InputDigitDataConsole(2);
            payInfo.EYear = !string.IsNullOrEmpty(eYear)
                ? payInfo.EYear = eYear
                : payInfo.EYear = "00";

            Console.WriteLine("Фамилия и имя держателя.\r");
            string? сardHolder = ConsoleOperator.InputStringDataConsole(30);
            payInfo.CardHolder = !string.IsNullOrEmpty(сardHolder)
                ? payInfo.CardHolder = сardHolder
                : payInfo.CardHolder = null;

            Console.WriteLine("CVC2/CVV2 код.\r");
            string? secureCode = ConsoleOperator.InputDigitDataConsole(int.MaxValue);
            payInfo.SecureCode = !string.IsNullOrEmpty(secureCode)
                ? payInfo.SecureCode = secureCode
                : payInfo.SecureCode = null;

            RequestPay.PayInfo = payInfo.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("Ошибка формирования данных для запроса", ex);
        }
    }
}
