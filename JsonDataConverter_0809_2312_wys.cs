// 代码生成时间: 2025-08-09 23:12:34
using System;
using System.Text.Json;
# 增强安全性
using System.Text.Json.Serialization;

namespace JsonDataConverterApp
{
    /// <summary>
    /// A utility class for converting JSON data.
    /// </summary>
    public class JsonDataConverter
# TODO: 优化性能
    {
        /// <summary>
        /// Converts an object to a JSON string.
        /// </summary>
        /// <param name="input">The object to convert.</param>
        /// <returns>The JSON string representation of the object.</returns>
        public string ObjectToJson(object input)
        {
            try
            {
                return JsonSerializer.Serialize(input, new JsonSerializerOptions { WriteIndented = true });
            }
            catch (JsonException ex)
            {
                // Handle serialization error
                Console.WriteLine($"Error serializing object to JSON: {ex.Message}");
                return null;
# 优化算法效率
            }
        }

        /// <summary>
        /// Converts a JSON string to an object of type T.
# 扩展功能模块
        /// </summary>
        /// <typeparam name="T">The type of object to convert to.</typeparam>
        /// <param name="json">The JSON string to convert.</param>
        /// <returns>The object of type T created from the JSON string.</returns>
        public T JsonToObject<T>(string json)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            catch (JsonException ex)
            {
                // Handle deserialization error
                Console.WriteLine($"Error deserializing JSON to object: {ex.Message}");
                return default;
            }
        }
    }
}
