using Microsoft.AspNetCore.Http;
using System.Text.Json;

public static class CookieExtensions
{
    public static void SetObjectAsJson(this IResponseCookies cookies, string key, object value, int? expireDays = null)
    {
        var options = new CookieOptions
        {
            Expires = expireDays.HasValue ? DateTimeOffset.Now.AddDays(expireDays.Value) : DateTimeOffset.Now.AddDays(1),
            IsEssential = true
        };
        cookies.Append(key, JsonSerializer.Serialize(value), options);
    }

    public static T? GetObjectFromJson<T>(this IRequestCookieCollection cookies, string key)
    {
        var cookie = cookies[key];
        return cookie != null ? JsonSerializer.Deserialize<T>(cookie) : default;
    }
}
