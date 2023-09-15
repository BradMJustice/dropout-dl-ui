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
