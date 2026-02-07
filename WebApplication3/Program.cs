var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var app = builder.Build();


app.UseRouting();

app.UseAuthorization();


app.MapControllers();


app.MapGet("/", () => "API is running. Go to /browser endpoint");


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

    var result = $"=== ÀÍÀËÈÇ BROWSER ===\n" +
                $"User-Agent: {userAgent}\n" +
                $"Áðàóçåð: {browser}\n" +
                $"Óñòðîéñòâî: {device}\n" +
                $"ÎÑ: {os}";

    return Results.Text(result, "text/plain");
});

app.Run();
