using PuppeteerSharp;
using System.Runtime.InteropServices;

namespace dropout_dl.Common.Logic
{
    public static class DropoutBrowser
    {
        private static string _chromiumPath = "";
        public static async Task InitializeBrowser()
        {
            var browserFetcherOptions = new BrowserFetcherOptions
            {
                Path = ChromiumPath()
            };
            using var browserFetcher = new BrowserFetcher(browserFetcherOptions);
            var chromiumRevision = PuppeteerSharp.BrowserData.Chrome.DefaultBuildId;
            var browser = await browserFetcher.DownloadAsync(chromiumRevision);
            _chromiumPath = browser.GetExecutablePath();
        }
        public static async Task<string> GetPageHtml(string url)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
            requestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            requestMessage.Headers.Add("Accept-Language", "en-US,en;q=0.9");
            requestMessage.Headers.Add("Cache-Control", "no-cache");
            requestMessage.Headers.Add("Dnt", "1");
            requestMessage.Headers.Add("Pragma", "no-cache");
            requestMessage.Headers.Add("Sec-Ch-Ua", "\"Chromium\";v=\"116\", \"Not)A;Brand\";v=\"24\", \"Google Chrome\";v=\"116\"");
            requestMessage.Headers.Add("Sec-Ch-Ua-Mobile", "?0");
            requestMessage.Headers.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
            requestMessage.Headers.Add("Sec-Fetch-Dest", "document");
            requestMessage.Headers.Add("Sec-Fetch-Mode", "navigate");
            requestMessage.Headers.Add("Sec-Fetch-Site", "none");
            requestMessage.Headers.Add("Sec-Fetch-User", "?1");
            requestMessage.Headers.Add("Upgrade-Insecure-Requests", "1");
            requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/116.0.0.0 Safari/537.36");

            using var client = new HttpClient();
            var response = await client.SendAsync(requestMessage);

            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = _chromiumPath
            });

            using var page = await browser.NewPageAsync();
            var navOptions = new NavigationOptions
            {
                WaitUntil = new WaitUntilNavigation[]
                {
                    WaitUntilNavigation.Networkidle0
                },
                Timeout = 10000
            };

            await page.GoToAsync(url, navOptions);

            var content = await page.GetContentAsync();
            return content;
        }
        private static string ChromiumPath()
        {
            return IsLinux()
                ? "/tmp"
                : Directory.GetCurrentDirectory();
        }
        public static bool IsLinux()
        {
            return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
        }
    }
}
