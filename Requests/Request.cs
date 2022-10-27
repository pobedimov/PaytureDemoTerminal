namespace PaytureDemoTerminal.Requests;

/// <summary>
/// Представляет базовый общий класс для платёжных операций Api.
/// </summary>
public class Request
{
    private string _orderId = string.Empty;

    /// <summary>
    /// Наименование платежного Терминала.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Уникальный идентификатор платежа в системе Продавца.
    /// </summary>
    public string OrderId
    {
        get => _orderId;
        set
        {
            if (value.Length < 1 && value.Length > 50)
                throw new ArgumentOutOfRangeException(nameof(OrderId), "OrderId lenght out of range.");

            _orderId = value;
        }
    }
}
