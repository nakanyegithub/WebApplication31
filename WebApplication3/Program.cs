var builder = WebApplication.CreateBuilder(args);

// Добавляем контроллеры (для Web API)
builder.Services.AddControllers();

var app = builder.Build();

// Включаем роутинг
app.UseRouting();

app.UseAuthorization();

// Маппим контроллеры
app.MapControllers();

// Добавляем простой endpoint для проверки
app.MapGet("/", () => "API is running. Go to /browser endpoint");

// Ваш endpoint /browser
app.MapGet("/browser", (HttpContext context) =>
{
    var userAgent = context.Request.Headers["User-Agent"].ToString();

    string browser = "Other";
    string device = "Desktop";
    string os = "Unknown";

    if (!string.IsNullOrEmpty(userAgent))
    {
        var ua = userAgent.ToLower();

        
        if (ua.Contains("chrome") && !ua.Contains("edg")) browser = "Chrome";
        else if (ua.Contains("firefox")) browser = "Firefox";
        else if (ua.Contains("safari") && !ua.Contains("chrome")) browser = "Safari";
        else if (ua.Contains("edg")) browser = "Edge";

        
        if (ua.Contains("mobile") || ua.Contains("android") || ua.Contains("iphone"))
            device = "Mobile";
        else if (ua.Contains("bot") || ua.Contains("crawler") || ua.Contains("spider"))
            device = "Bot";

        
        if (ua.Contains("windows")) os = "Windows";
        else if (ua.Contains("mac os") || ua.Contains("macos")) os = "macOS";
        else if (ua.Contains("linux") && !ua.Contains("android")) os = "Linux";
        else if (ua.Contains("ios") || ua.Contains("iphone")) os = "iOS";
        else if (ua.Contains("android")) os = "Android";
    }

    var result = $"=== АНАЛИЗ BROWSER ===\n" +
                $"User-Agent: {userAgent}\n" +
                $"Браузер: {browser}\n" +
                $"Устройство: {device}\n" +
                $"ОС: {os}";

    return Results.Text(result, "text/plain");
});

app.Run();