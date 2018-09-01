using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CSNAlbum
{
    public class AlbumParser
    {
        public List<string> ParseAlbum(string url)
        {
            //var result = new List<string>();
            //var web = new HtmlWeb();
            //var document = web.Load(url);
            //var page = document.DocumentNode;
            //var pageContent = page.InnerHtml;
            //var regEx = @"<a\s+(?:[^>]*?\s+)?href=([" + '\"'+ "'])(.*?)\\1";
            //Regex r = new Regex(regEx, RegexOptions.IgnoreCase);
            //Match m = r.Match(pageContent);
            //int count = 0;
            //while (m.Success)
            //{
            //    ++count;
            //    if (m.Value.Contains("download.html")) result.Add(m.Value);
            //    m = m.NextMatch();
            //}
            //return result;

            return ParseLink(url, ".html").Where(q => q.Contains("download.html")).ToList();
        }

        public List<string> GetMp3Links(string url)
        {
            var prefix = "<a href=\"";
            var mp3Url = url.Substring(prefix.Length, url.Length - prefix.Length - 1);
            var result = ParseLink(mp3Url, ".mp3");
            return result.Where(q => q.Contains(".mp3") || q.Contains(".m4a") || q.Contains(".flac")).ToList(); ;
        }

        private List<string> ParseLink(string url,string suffix)
        {
            var result = new List<string>();
            var web = new HtmlWeb();
            var document = web.Load(url);
            var page = document.DocumentNode;
            var pageContent = page.InnerHtml;
            var regEx = @"<a\s+(?:[^>]*?\s+)?href=([" + '\"' + "'])(.*?)\\1";
            Regex r = new Regex(regEx, RegexOptions.IgnoreCase);
            Match m = r.Match(pageContent);
            int count = 0;
            while (m.Success)
            {
                if (m.Value.Contains(suffix)) result.Add(m.Value);
                m = m.NextMatch();
            }
            return result;
        }
    }
}
