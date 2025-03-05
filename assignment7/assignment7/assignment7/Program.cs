using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace assignment7
{
    internal class Program
    {
        private static Hashtable urls = new Hashtable();
        private static int count = 0;
        private static readonly string saveDirectory = @"D:\csassignment";
        private static readonly object lockObj = new object();

        static void Main(string[] args)
        {
            Directory.CreateDirectory(saveDirectory);
            string startUrl = "http://www.cnblogs.com/dstang2000/";
            if (args.Length >= 1) startUrl = args[0];

            lock (lockObj)
            {
                urls.Add(startUrl, false);
            }

            new Thread(Crawl).Start();
        }

        private static void Crawl()
        {
            Console.WriteLine("开始爬行......");
            while (true)
            {
                string current = null;

                lock (lockObj)
                {
                    foreach (string url in urls.Keys)
                    {
                        if ((bool)urls[url]) continue;
                        current = url;
                        break;
                    }
                }

                if (current == null || count > 10) break;

                Console.WriteLine($"爬行 {current} 页面!");
                string html = Download(current);

                lock (lockObj)
                {
                    urls[current] = true;
                }

                count++;
                Parse(html, current); // 传递当前页URL用于解析相对路径
            }
            Console.WriteLine("爬行结束");
        }

        public static string Download(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AllowAutoRedirect = true; // 启用自动重定向
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64)";

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    string html = reader.ReadToEnd();
                    string fileName = $"{count}.html";
                    string filePath = Path.Combine(saveDirectory, fileName);
                    File.WriteAllText(filePath, html, Encoding.UTF8);
                    return html;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"下载失败 [{url}]: {ex.Message}");
                return "";
            }
        }

        public static void Parse(string html, string currentUrl)
        {
            string pattern = @"(href|HREF)\s*=\s*[""'](.*?)[""']";
            MatchCollection matches = Regex.Matches(html, pattern);
            Uri baseUri = new Uri(currentUrl);

            foreach (Match match in matches) // 遍历每个Match对象
            {
                // 获取第二个捕获组的值（对应正则表达式中的(.*?)部分）
                string extractedUrl = match.Groups[2].Value.Trim();
                if (string.IsNullOrEmpty(extractedUrl)) continue;

                try
                {
                    Uri fullUri = new Uri(baseUri, extractedUrl);
                    string absoluteUrl = fullUri.AbsoluteUri;

                    lock (lockObj)
                    {
                        if (!urls.ContainsKey(absoluteUrl))
                        {
                            urls.Add(absoluteUrl, false);
                            Console.WriteLine($"发现新链接: {absoluteUrl}");
                        }
                    }
                }
                catch (UriFormatException)
                {
                    Console.WriteLine($"无效URL格式: {extractedUrl}");
                }
            }
        }
    }
}