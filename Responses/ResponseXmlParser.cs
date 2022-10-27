using System.Text;
using System.Xml;

namespace PaytureDemoTerminal.Responses;

/// <summary>
/// Представляет класс для парсинга XML ответа от Api.
/// </summary>
public static class ResponseXmlParser
{
    /// <summary>
    /// Выполняет парсинг Xml ответа.
    /// </summary>
    /// <param name="xmlDoc">Входящие Xml данные.</param>
    /// <returns>Строка для вывода.</returns>
    /// <exception cref="XmlException">Входящие данные содержат некорректный формат XML.</exception>
    /// <exception cref="FormatException">Входящие данные содержат узлов XML.</exception>
    public static string Parse(string xmlDoc)
    {
        if (string.IsNullOrEmpty(xmlDoc))
            return string.Empty;

        StringBuilder stringBuilder = new();

        try
        {
            // Разбор XML данных.
            XmlDocument xDoc = new();
            xDoc.LoadXml(xmlDoc);

            // Возвращает все дочерние узлы данного узла
            XmlElement? xNode = xDoc.DocumentElement;
            if (xNode is not null)
            {
                // Обходим все аттрибуты узла элемента
                foreach (XmlAttribute attributes in xNode.Attributes)
                {
                    stringBuilder.AppendLine($"{attributes.Name} = {attributes.Value}");
                }

                // Если есть узлы с дополнительной информацией AddInfo.
                if (xNode.HasChildNodes)
                {
                    foreach (XmlNode itemAddInfoNode in xNode.ChildNodes)
                    {
                        if (itemAddInfoNode.Attributes is not null)
                        {
                            foreach (XmlAttribute attributes in itemAddInfoNode.Attributes)
                            {
                                stringBuilder.Append($"{attributes.Name} = {attributes.Value} ");
                            }
                        }
                        stringBuilder.AppendLine();
                    }
                }

                return stringBuilder.ToString();
            }
            else
            {
                throw new FormatException("Полученный ответ от Api содержит корректный формат.");
            }
        }
        catch (XmlException)
        {
            throw;
        }
    }
}
