using dropout_dl.Common.Logic;
using HtmlAgilityPack;

namespace dropout_dl_ui.Tests
{
    public class AuthTests
    {
        [Fact]
        public async Task TestLoginWithCredentials()
        {
            await DropoutBrowser.InitializeBrowser();
            var html = await DropoutBrowser.GetPageHtml("https://www.dropout.tv/login");

            var document = new HtmlDocument();
            document.LoadHtml(html);

            var node = document.DocumentNode.SelectSingleNode("//h1");
        }
    }
}