using System.Text.Json;
using Common.Contracts;

namespace Common.JsonExtensions;

public static class DeserializerExtension
{
    public static T Serialize<T>(this string toDeserialize)
        where T : class
    {
        T deserializedObject = JsonSerializer.Deserialize<T>(toDeserialize);
        if (deserializedObject == null)
            throw new InvalidOperationException();
        
        return deserializedObject;
    }
}