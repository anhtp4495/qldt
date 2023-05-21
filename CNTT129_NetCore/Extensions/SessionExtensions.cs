using System.Text.Json;

namespace CNTT129_NetCore.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            try
            {
                var value = session.GetString(key);
                return value == null ? default(T) : JsonSerializer.Deserialize<T>(value);
            }
            catch(Exception e)
            { }

            return default(T);
        }
        public static T? Get<T>(this CustomSession session, string key)
        {
            return session.Session.Get<T>(key);
        }
    }
}
