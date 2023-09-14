using dropout_dl.Common.Enums;
using dropout_dl.Common.Logic;

namespace dropout_dl_ui.Tests
{
    public class DownloadTests
    {
        [Fact]
        public void TestUrlsParseToCorrectContentType()
        {
            var episodeUrl = "https://www.dropout.tv/make-some-noise/season:1/videos/hbo-s-a-game-of-rock-paper-scissors";
            var seasonUrl = "https://www.dropout.tv/make-some-noise/season:1";
            var seriesUrl = "https://www.dropout.tv/make-some-noise";

            Assert.Equal(DropoutContentType.Episode, DownloadLogic.GetContentTypeFromUrl(episodeUrl));
            Assert.Equal(DropoutContentType.Season, DownloadLogic.GetContentTypeFromUrl(seasonUrl));
            Assert.Equal(DropoutContentType.Series, DownloadLogic.GetContentTypeFromUrl(seriesUrl));
        }
    }
}