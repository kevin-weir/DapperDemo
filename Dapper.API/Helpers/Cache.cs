using System.Text.Json;

namespace Dapper.API.Helpers
{
    public static class Cache<T>
    {
        // Convert an object to a byte array
        public static byte[] ObjectToByteArray(T obj)
        {
            return JsonSerializer.SerializeToUtf8Bytes<T>(obj);
        }

        // Convert a byte array to an Object
        public static T ByteArrayToObject(byte[] arrBytes)
        {
            var utf8Reader = new Utf8JsonReader(arrBytes);
            return JsonSerializer.Deserialize<T>(ref utf8Reader);
        }
    }
}
