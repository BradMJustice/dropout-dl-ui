using dropout_dl.Common.Enums;
using dropout_dl.Common.Logic;

namespace dropout_dl_ui.Tests
{
    public class DownloadTests
    {
        [Theory]
        [InlineData("https://www.dropout.tv/dirty-laundry/season:1/videos/who-stayed-up-for-81-hours")]
        [InlineData("https://www.dropout.tv/dimension-20/season:1/videos/episode-1")]
        [InlineData("https://www.dropout.tv/make-some-noise/season:1/videos/hbo-s-a-game-of-rock-paper-scissors")]
        public void TestEpisodeUrlParsesToCorrectContentType(string url)
        {
            var actualContentType = DownloadLogic.GetContentTypeFromUrl(url);
            Assert.Equal(DropoutContentType.Episode, actualContentType);
        }
        [Theory]
        [InlineData("https://www.dropout.tv/dirty-laundry/season:1")]
        [InlineData("https://www.dropout.tv/dimension-20/season:5")]
        [InlineData("https://www.dropout.tv/make-some-noise/season:2")]
        public void TestSeasonUrlParsesToCorrectContentType(string url)
        {
            var actualContentType = DownloadLogic.GetContentTypeFromUrl(url);
            Assert.Equal(DropoutContentType.Season, actualContentType);
        }
        [Theory]
        [InlineData("https://www.dropout.tv/dirty-laundry")]
        [InlineData("https://www.dropout.tv/dimension-20")]
        [InlineData("https://www.dropout.tv/make-some-noise")]
        public void TestSeriesUrlParsesToCorrectContentType(string url)
        {
            var actualContentType = DownloadLogic.GetContentTypeFromUrl(url);
            Assert.Equal(DropoutContentType.Series, actualContentType);
        }
    }
}