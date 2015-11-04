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
            return doc.DocumentNode
                .SelectNodes(@"a[class=""yt-uix-tile-link""]")
                .Where((node) => {
                    var href = node.Attributes["href"];
                    return href != null && href.Value.Contains("/watch?v=");
                }).Select((node) => {
                    return new Hyperlink
                    {
                        Href = new Uri(node.Attributes["href"].Value),
                        Text = node.InnerText               
                    };
                });
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
