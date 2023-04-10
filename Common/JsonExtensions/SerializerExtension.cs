using System.Text.Json;

namespace Common.JsonExtensions;

public static class SerializerExtension
{
    public static string Serialize<T>(this object toSerialize)
        where T : class
    {
        var data = toSerialize as T;

        if (data == null)
            throw new InvalidOperationException();
        
        return JsonSerializer.Serialize<T>(data);
    }
}