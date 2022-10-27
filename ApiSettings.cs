namespace PaytureDemoTerminal;

/// <summary>
/// Представялет настройки Api, включающие домен и URI.
/// </summary>
public static class ApiSettings
{
    /// <summary>
    /// Основной домен Api.
    /// </summary>
    public static string ApiDomain { get; set; } = "payture.com";

    /// <summary>
    /// Программная среда, предоставляется с параметрами тестового и коммерческого доступа.
    /// </summary>
    public static string ApiEnvironment { get; set; } = "sandbox3";

    /// <summary>
    /// Используемый программный интерфейс.
    /// </summary>
    public static string ApiInterface { get; set; } = "api";

    /// <summary>
    /// Название выполняемой команды.
    /// </summary>
    public static string ApiCommand { get; set; } = "Pay";


    /// <summary>
    /// Получает полный адрес URI для выполнения команды в соответствии с заданными настройками.
    /// </summary>
    /// <exception cref="UriFormatException">Ошибка формата Uri.</exception>
    /// <exception cref="FormatException">Некорректно задан формат Uri.</exception>
    /// <returns>Возвращает полный адрес URI для выполняемой команды Api.</returns>
    public static Uri GetFullApiAddress()
    {
        if (string.IsNullOrWhiteSpace(ApiDomain) && string.IsNullOrWhiteSpace(ApiEnvironment) && string.IsNullOrWhiteSpace(ApiInterface) && !string.IsNullOrWhiteSpace(ApiCommand))
        {
            throw new FormatException("Некорректно задан формат Uri");
        }

        return new Uri($"https://{ApiEnvironment}.{ApiDomain}/{ApiInterface}/{ApiCommand}");
    }
}
