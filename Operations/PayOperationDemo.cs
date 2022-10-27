using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace PaytureDemoTerminal.Operations;

/// <summary>
/// Представляет класс для демонстрации операции Pay с данными из документации с сайта payture.com
/// </summary>
public sealed class PayOperationDemo : IPayOperation
{
    /// <summary>
    /// Представляет адрес Api для выполнения операции Pay.
    /// </summary>
    public Uri ApiUri { get; private set; }

    /// <summary>
    /// Конструктор класса.
    /// </summary>
    public PayOperationDemo()
    {
        // Установка настроек и получение URI Api.
        ApiSettings.ApiDomain = "payture.com";
        ApiSettings.ApiEnvironment = "sandbox3";
        ApiSettings.ApiInterface = "api";
        ApiSettings.ApiCommand = "Pay";

        ApiUri = ApiSettings.GetFullApiAddress();
    }

    /// <summary>
    /// Отображение запроса.
    /// </summary>
    public void DisplayOperationInfo()
    {
        Console.WriteLine("Сформирован запрос со следующими данными для операции Pay Payture Api:\n\r");
        Console.WriteLine($"{ApiUri}\n\r");
        Console.WriteLine("Key=Merchant\n\rAmount=12500\n\rOrderId=21c34d21-3111-c91a-c7d3-fa0b78054b\n\r");
        Console.WriteLine("PAN=5218851946955484;\n\rEMonth=12;\n\rEYear=22;\n\rCardHolder=Ivan Ivanov;\n\rSecureCode=123;\n\rOrderId=21c34d21-3111-c91a-c7d3-fa0b78054bf1;\n\rAmount=12500\n\r");
        Console.WriteLine("IP=74.11.229.64;\n\rProduct=Ticket\n\r");
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
        // Формирование параметров.
        string payInfo = HttpUtility.UrlEncode("PAN=5218851946955484; EMonth=12; EYear=22; CardHolder=Ivan Ivanov; SecureCode=123; OrderId=21c34d21-3111-c91a-c7d3-fa0b78054bf1; Amount=12500");
        string additionalInfo = HttpUtility.UrlEncode("IP=74.11.229.64; Product=Ticket");

        // Формирование запроса.
        HttpContent httpContent = new StringContent($"Key=Merchant&Amount=12500&OrderId=21c34d21-3111-c91a-c7d3-fa0b78054bf&PayInfo={payInfo}&CustomFields={additionalInfo}",
                                        Encoding.UTF8, "application/x-www-form-urlencoded");

        return await Operation.ExecuteAsync(ApiUri, httpContent, new MediaTypeWithQualityHeaderValue("application/xml"));
    }
}
