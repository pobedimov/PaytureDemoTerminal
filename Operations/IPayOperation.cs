namespace PaytureDemoTerminal.Operations;

/// <summary>
/// Представляет интерфейс платежной операции.
/// </summary>
public interface IPayOperation
{
    /// <summary>
    /// Представляет адрес Api для выполнения операции Pay.
    /// </summary>
    Uri ApiUri { get; }

    /// <summary>
    /// Отображение запроса.
    /// </summary>
    void DisplayOperationInfo();

    /// <summary>
    /// Отправляет запрос на выполнение операции Pay.
    /// </summary>
    /// <returns>Ответ от Api на операцию Pay.</returns>
    Task<string> PayAsync();
}