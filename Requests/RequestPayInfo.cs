using System.Text;

namespace PaytureDemoTerminal.Requests;

/// <summary>
/// Преставляет информацию о параметрах для совершения транзакции.
/// </summary>
public class RequestPayInfo
{
    private string _pan = string.Empty;
    private string _eMonth = string.Empty;
    private string _eYear = string.Empty;
    private string? _cardHolder;

    /// <summary>
    /// Номер карты.
    /// </summary>
    public string PAN
    {
        get => _pan;
        set
        {
            if (value.Length < 13 && value.Length > 19)
                throw new ArgumentOutOfRangeException(nameof(PAN), "PAN lenght out of range.");

            _pan = value;
        }
    }

    /// <summary>
    /// Месяц истечения срока действия карты.
    /// </summary>
    public string EMonth
    {
        get => _eMonth;
        set
        {
            if ( value.Length < 2 && value.Length > 3)
                throw new ArgumentOutOfRangeException(nameof(EMonth), "EMonth lenght or value out of range.");

            _eMonth = value;
        }
    }

    /// <summary>
    /// Год истечения срока действия карты.
    /// </summary>
    public string EYear
    {
        get => _eYear;
        set
        {
            if (value.Length < 2 && value.Length > 3)
                throw new ArgumentOutOfRangeException(nameof(EYear), "EYear lenght or value out of range.");

            _eYear = value;
        }
    }

    /// <summary>
    /// Фамилия и имя держателя карты.
    /// </summary>
    public string? CardHolder
    {
        get => _cardHolder;
        set
        {
            if (value?.Length < 1 && value.Length > 30)
                throw new ArgumentOutOfRangeException(nameof(_cardHolder), "CardHolderlenght out of range.");
            if (value is not null)
                _cardHolder = value;
        }
    }

    /// <summary>
    /// CVC2/CVV2 код.
    /// </summary>
    public string? SecureCode { get; set; }

    /// <summary>
    /// Уникальный идентификатор платежа в системе Продавца.
    /// </summary>
    public string OrderId { get; set; } = string.Empty;

    /// <summary>
    /// Сумма платежа в копейках (или другая минимальная единица валюты терминала).
    /// </summary>
    public string Amount { get; set; } = string.Empty;


    /// <summary>
    /// Сохраняет данные в строковом формате.
    /// </summary>
    /// <returns>Сформированная строка с параметрами.</returns>
    public override string ToString()
    {
        StringBuilder stringBuilder = new();
        stringBuilder.AppendLine($"PAN={PAN}; ");
        stringBuilder.AppendLine($"EMonth={EMonth}; ");
        stringBuilder.AppendLine($"EYear={EYear}; ");
        if (CardHolder is not null)
            stringBuilder.AppendLine($"CardHolder={CardHolder}; ");
        if (SecureCode is not null)
            stringBuilder.AppendLine($"SecureCode={SecureCode}; ");
        stringBuilder.AppendLine($"OrderId={OrderId}; ");
        stringBuilder.AppendLine($"Amount={Amount}; ");

        return stringBuilder.ToString();
    }
}
