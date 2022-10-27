using System.Net.Http.Headers;

namespace PaytureDemoTerminal.Operations;

/// <summary>
/// Отправляет операцию и получает ответ.
/// </summary>
public static class Operation
{
    /// <summary>
    /// Выполняет операцию/запрос.
    /// </summary>
    /// <param name="uri">Адрес URI к Api</param>
    /// <param name="httpContent">HttpContent данные для отправки.</param>
    /// <param name="mediaTypeWithQualityHeaderValue">Тип получаемых в ответ данных.</param>
    /// <exception cref="HttpRequestException">HTTP-ответ завершился неудачей.</exception>
    /// <exception cref="ArgumentNullException">В качестве входящего параметра передана пустая ссылка.</exception>
    /// <returns>Ответ от Api на операцию в виде строки.</returns>
    public static async Task<string> ExecuteAsync(Uri uri, HttpContent httpContent, MediaTypeWithQualityHeaderValue mediaTypeWithQualityHeaderValue)
    {
        if (uri is null)
        {
            throw new ArgumentNullException(nameof(uri) ,"В качестве входящего параметра передана пустая ссылка.");
        }
        if (httpContent is null)
        {
            throw new ArgumentNullException(nameof(httpContent), "В качестве входящего параметра передана пустая ссылка.");
        }
        if(mediaTypeWithQualityHeaderValue is null)
        {
            throw new ArgumentNullException(nameof(mediaTypeWithQualityHeaderValue), "В качестве входящего параметра передана пустая ссылка.");
        }


        using HttpClient client = new();
        client.BaseAddress = uri;
        client.DefaultRequestHeaders.Accept.Add(mediaTypeWithQualityHeaderValue);

        var response = await client.PostAsync(uri, httpContent);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}
