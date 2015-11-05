using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Libnimbus;

namespace NimbusCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            string html = File.ReadAllText(@"C:\Users\14581\Libnimbus\fixtures\youtube-modestep.html");
            Youtube yt = new Youtube();
            var links = yt.ExtractVideoLinks(html);
            var indexedLinks = links.Select((link, i) => {
                return new
                {
                    Index = i,
                    Link = link
                };
            });
            foreach (var linkItem in indexedLinks)
            {
                Console.WriteLine("[{0}]: {1}", linkItem.Index, linkItem.Link.Text);
            }
            UInt32 selectedIx;
            do
            {
                Console.Write("Which one do you want? ");
            } while (!UInt32.TryParse(Console.ReadLine(), out selectedIx));

            Console.ReadLine();

        }
    }
}
