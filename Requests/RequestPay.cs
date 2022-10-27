using System.Text;
using System.Web;

namespace PaytureDemoTerminal.Requests;

/// <summary>
/// Представляет запрос для операции "Pay" Api Payture.
/// </summary>
public class RequestPay : Request
{
    private string? _paytureId;
    private string? _customerKey;

    /// <summary>
    /// Сумма платежа в копейках (или другая минимальная единица валюты терминала).
    /// </summary>
    public string Amount { get; set; } = string.Empty;

    /// <summary>
    /// Параметры для совершения транзакции.
    /// </summary>
    public string PayInfo { get; set; } = string.Empty;

    /// <summary>
    /// Идентификатор платежа в системе Payture AntiFraud.
    /// </summary>
    public string? PaytureId
    {
        get => _paytureId;
        set
        {
            if (value?.Length < 1 && value.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(_paytureId), "PaytureId lenght out of range.");
            if (value is not null)
                _paytureId = value;
        }
    }

    /// <summary>
    /// Идентификатор Покупателя в системе Payture AntiFraud.
    /// </summary>
    public string? CustomerKey
    {
        get => _customerKey;
        set
        {
            if (value?.Length < 1 && value.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(_customerKey), "CustomerKey lenght out of range.");
            if (value is not null)
                _customerKey = value;
        }
    }

    /// <summary>
    /// Дополнительные поля транзакции.
    /// </summary>
    public string? CustomFields { get; set; }

    /// <summary>
    /// Информация о чеке в формате JSON, закодированная в Base64.
    /// </summary>
    public string? Cheque { get; set; }

    /// <summary>
    /// Возвращает сформированный Uri для выполнения операции Pay.
    /// </summary>
    /// <returns>Строка запроса в формате Uri.</returns>
    public string GetUriPayRequest()
    {
        StringBuilder stringBuilder = new();

        string payInfo = HttpUtility.UrlEncode(PayInfo);
        string paytureId = string.Empty;
        string customerKey = string.Empty;
        string customFields = string.Empty;
        string cheque = string.Empty;
        if (PaytureId is not null) paytureId = HttpUtility.UrlEncode(PaytureId);
        if (CustomerKey is not null) customerKey = HttpUtility.UrlEncode(CustomerKey);
        if (CustomFields is not null) customFields = HttpUtility.UrlEncode(CustomFields);
        if (Cheque is not null) cheque = HttpUtility.UrlEncode(Cheque);

        stringBuilder.Append($"Key={Key}&");
        stringBuilder.Append($"Amount={Amount}&");
        stringBuilder.Append($"OrderId={OrderId}&");
        stringBuilder.Append($"PayInfo={payInfo}");
        if (!string.IsNullOrEmpty(paytureId))
            stringBuilder.Append($"&PaytureId={paytureId}");
        if (!string.IsNullOrEmpty(customerKey))
            stringBuilder.Append($"&CustomerKey={customerKey}");
        if (!string.IsNullOrEmpty(customFields))
            stringBuilder.Append($"&CustomFields={customFields}");
        if (!string.IsNullOrEmpty(cheque))
            stringBuilder.Append($"&Cheque={cheque}");

        return stringBuilder.ToString();
    }
}
