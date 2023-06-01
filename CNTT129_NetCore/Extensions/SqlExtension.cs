using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace CNTT129_NetCore.Extensions
{
    public static class SqlExtension
    {
        public static string ToSqlDateTime(this string datetime, DateTime defaultDatetime)
        {
            try
            {
                DateTime dateTimeParsed = DateTime.ParseExact(datetime, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                return dateTimeParsed.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch
            {
            }
            return defaultDatetime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static int GetInt32OrDefault(this SqlDataReader dr, string name)
        {
            try
            {
                if (!dr.IsDBNull(name)) return dr.GetInt32(name);
            }
            catch { }
            return 0;
        }

        public static string GetStringOrDefault(this SqlDataReader dr, string name)
        {
            try
            {
                if (!dr.IsDBNull(name)) return dr.GetString(name);
            }
            catch { }
            return string.Empty;
        }

        public static List<string> GetListStringOrDefault(this SqlDataReader dr, string name)
        {
            try
            {
                if (!dr.IsDBNull(name))
                {
                    string str = dr.GetString(name);
                    return str.Split(new[] { ',' }).ToList();
                }
            }
            catch { }
            return new List<string>();
        }

        public static string ConvertLoai2TenBuoi(this int loai)
        {
            switch (loai)
            {
                case 1: return "Sáng (7h-11h)";
                case 2: return "Chiều (13h-17h)";
                case 3: return "Tối (18h-22h)";
                default: return "Cả ngày";
            }
        }
    }
}
