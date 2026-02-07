using WebApplication3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BrowserAnalyzer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrowserController : ControllerBase
    {
        [HttpGet("browser")]
        public IActionResult AnalyzeBrowser()
        {
            var userAgent = Request.Headers["User-Agent"].ToString();
            var browserInfo = new BrowserInfo();

            if (!string.IsNullOrEmpty(userAgent))
            {
                browserInfo.Browser = DetermineBrowser(userAgent);
                browserInfo.Device = DetermineDevice(userAgent);
                browserInfo.OS = DetermineOS(userAgent);
            }

            
            var result = $"=== АНАЛИЗ BROWSER ===\n" +
                        $"User-Agent: {userAgent}\n" +
                        $"Браузер: {browserInfo.Browser}\n" +
                        $"Устройство: {browserInfo.Device}\n" +
                        $"ОС: {browserInfo.OS}";

            return Ok(result);
        }

        private string DetermineBrowser(string userAgent)
        {
            userAgent = userAgent.ToLower();

            
            if (userAgent.Contains("edg"))
                return "Edge";
            if (userAgent.Contains("chrome") && !userAgent.Contains("edg"))
                return "Chrome";
            if (userAgent.Contains("firefox"))
                return "Firefox";
            if (userAgent.Contains("safari") && !userAgent.Contains("chrome"))
                return "Safari";
            if (userAgent.Contains("opera") || userAgent.Contains("opr"))
                return "Opera";
            if (userAgent.Contains("trident") || userAgent.Contains("msie"))
                return "Internet Explorer";

            return "Other";
        }

        private string DetermineDevice(string userAgent)
        {
            userAgent = userAgent.ToLower();

            
            if (userAgent.Contains("bot") ||
                userAgent.Contains("crawler") ||
                userAgent.Contains("spider") ||
                userAgent.Contains("scraper"))
                return "Bot";

          
            if (userAgent.Contains("mobile") ||
                userAgent.Contains("android") ||
                userAgent.Contains("iphone") ||
                userAgent.Contains("ipod") ||
                userAgent.Contains("blackberry") ||
                userAgent.Contains("windows phone"))
                return "Mobile";

            
            if (userAgent.Contains("tablet") ||
                userAgent.Contains("ipad") ||
                userAgent.Contains("kindle"))
                return "Tablet";

            return "Desktop";
        }

        private string DetermineOS(string userAgent)
        {
            userAgent = userAgent.ToLower();

            if (userAgent.Contains("windows"))
            {
                if (userAgent.Contains("windows nt 10") || userAgent.Contains("windows 10"))
                    return "Windows 10";
                if (userAgent.Contains("windows nt 11") || userAgent.Contains("windows 11"))
                    return "Windows 11";
                return "Windows";
            }
            if (userAgent.Contains("mac os") || userAgent.Contains("macos"))
                return "macOS";
            if (userAgent.Contains("linux") && !userAgent.Contains("android"))
                return "Linux";
            if (userAgent.Contains("ios") || userAgent.Contains("iphone") || userAgent.Contains("ipad"))
                return "iOS";
            if (userAgent.Contains("android"))
                return "Android";

            return "Unknown";
        }
    }
}