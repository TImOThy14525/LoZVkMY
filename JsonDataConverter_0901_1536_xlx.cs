// 代码生成时间: 2025-09-01 15:36:49
 * It includes error handling and is designed to be easy to understand and maintain.
 *
 * Author: Your Name
 * Date: Today's Date
 */
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

// Exception specific to JSON data conversion errors.
public class JsonDataConversionException : Exception
{
    public JsonDataConversionException(string message) : base(message)
    {
    }
}

public class JsonDataConverter
{
    // Converts a JSON string to a C# object of type T.
    public T ConvertJsonStringToObject<T>(string jsonString)
    {
        if (string.IsNullOrWhiteSpace(jsonString))
        {
            throw new JsonDataConversionException("Invalid JSON string provided.");
        }

        try
        {
            return JsonSerializer.Deserialize<T>(jsonString);
        }
        catch (JsonException ex)
        {
            throw new JsonDataConversionException($"Error converting JSON to object: {ex.Message}");
        }
    }

    // Converts a C# object to a JSON string.
    public string ConvertObjectToJsonString<T>(T obj)
    {
        if (obj == null)
        {
            throw new JsonDataConversionException("Cannot convert null object to JSON string.");
        }

        try
        {
            return JsonSerializer.Serialize(obj);
        }
        catch (JsonException ex)
        {
            throw new JsonDataConversionException($"Error converting object to JSON string: {ex.Message}");
        }
    }
}
