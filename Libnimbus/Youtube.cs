using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Net.Http;

namespace Libnimbus
{
    public class Youtube
    {
        public IEnumerable<Hyperlink> ExtractVideoLinks(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            var nodes = doc.DocumentNode.SelectNodes(@"//a");
            var watchLinkNodes = nodes.Where((node) =>
            {
                var href = node.Attributes["href"];
                var htmlClass = node.Attributes["class"];
                return (
                    href != null &&
                    href.Value.Contains("/watch?v=") &&
                    htmlClass != null &&
                    htmlClass.Value.Contains("yt-uix-tile-link")
                );
            });
            var links = watchLinkNodes.Select((node) =>
            {
                return new Hyperlink
                {
                    Href = node.Attributes["href"].Value,
                    Text = node.InnerText
                };
            });
            return links;
        }

        public async Task<string> SearchVideos(string query)
        {
            var uri = new UriBuilder("https", "www.youtube.com")
                {
                    Path = "results",
                    Query = string.Format("search_query={}", query)
                }.Uri;

            HttpClient client = new HttpClient();
            return await client.GetStringAsync(uri);
        }
    }
}
