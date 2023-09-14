using dropout_dl.Common.Enums;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace dropout_dl.Common.Logic
{
    public class DownloadLogic
    {
        public static DropoutContentType GetContentTypeFromUrl(string url)
        {
            var seasonRegex = new Regex("season:\\d+\\/?$", RegexOptions.Compiled);
            var episodeRegex = new Regex("/videos/", RegexOptions.Compiled);

            if (seasonRegex.IsMatch(url))
            {
                return DropoutContentType.Season;
            }
            else if (episodeRegex.IsMatch(url))
            {
                return DropoutContentType.Episode;
            }
            else
            {
                return DropoutContentType.Series;
            }
        }
        public async Task Download(string url)
        {
            var contentType = GetContentTypeFromUrl(url);

            switch (contentType)
            {
                case DropoutContentType.Series:
                    await DownloadSeries(url, true, true);
                    break;
                case DropoutContentType.Season:
                    await DownloadSeason(url, true, true);
                    break;
                case DropoutContentType.Episode:
                default:
                    await DownloadEpisode(url, true, true);
                    break;
            }
        }
        public async Task DownloadEpisode(string url, bool getVideo, bool getCaptions)
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var document = new HtmlDocument();
            document.LoadHtml(html);

            var node = document.DocumentNode.SelectSingleNode("//h1");
        }
        public async Task DownloadSeason(string url, bool getVideo, bool getCaptions)
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var document = new HtmlDocument();
            document.LoadHtml(html);

            var node = document.DocumentNode.SelectSingleNode("//h1");
        }
        public async Task DownloadSeries(string url, bool getVideo, bool getCaptions)
        {
            var httpClient = new HttpClient();
            var html = await httpClient.GetStringAsync(url);

            var document = new HtmlDocument();
            document.LoadHtml(html);

            var node = document.DocumentNode.SelectSingleNode("//h1");
        }
    }
}
