using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ParallelWebCrawler
{
    class Program
    {
        // 可以修改的配置参数
        private const int MaxParallelism = 5;          // 同时最多开5个下载任务
        private const int MaxPages = 20;               // 最多下载20个页面
        private static readonly string SaveDirectory = @"D:\csassignment"; // 存文件的目录
        private static readonly Uri StartUri = new Uri("http://www.cnblogs.com/dstang2000/"); // 起始网址

        // 线程安全的集合，用来存网址
        private static readonly ConcurrentDictionary<Uri, bool> ProcessedUrls =
            new ConcurrentDictionary<Uri, bool>(); // 记录哪些网址已经抓过了
        private static readonly BlockingCollection<Uri> UrlQueue =
            new BlockingCollection<Uri>(new ConcurrentQueue<Uri>()); // 待抓取的网址队列

        private static int _processedCount; // 已经处理了多少页面

        static async Task Main(string[] args)
        {
            // 创建保存目录
            Directory.CreateDirectory(SaveDirectory);
            // 初始化爬虫
            InitializeCrawler();

            // 启动多个任务同时工作
            var consumerTasks = new Task[MaxParallelism];
            for (int i = 0; i < MaxParallelism; i++)
            {
                consumerTasks[i] = Task.Run(ProcessUrlsAsync);
            }

            // 等待所有任务完成
            await Task.WhenAll(consumerTasks);
            Console.WriteLine("所有任务已完成");
        }

        // 初始化爬虫，把第一个网址放进队列
        static void InitializeCrawler()
        {
            ProcessedUrls.TryAdd(StartUri, false); // 标记起始网址未处理
            UrlQueue.Add(StartUri); // 加入队列
            Console.WriteLine($"爬虫已启动 | 起始网址: {StartUri}");
        }

        // 每个工作线程的处理流程
        static async Task ProcessUrlsAsync()
        {
            // 当未达到最大数量且队列不为空时循环
            while (_processedCount < MaxPages && !UrlQueue.IsCompleted)
            {
                // 从队列取网址，最多等1秒钟
                if (UrlQueue.TryTake(out Uri currentUri, 1000))
                {
                    try
                    {
                        // 下载网页内容
                        var html = await DownloadHtmlAsync(currentUri);
                        if (!string.IsNullOrEmpty(html))
                        {
                            // 原子操作增加计数器
                            Interlocked.Increment(ref _processedCount);
                            // 解析页面中的链接
                            ParseAndEnqueueLinks(html, currentUri);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"[处理失败] {currentUri}\n原因: {ex.Message}");
                    }
                }
            }
        }

        // 真正下载网页的地方，加了超时和重试
        static async Task<string> DownloadHtmlAsync(Uri uri)
        {
            using var client = new HttpClient();
            // 假装是浏览器，防止被网站屏蔽
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

            try
            {
                // 发送请求
                var response = await client.GetAsync(uri);
                // 检查是否成功
                response.EnsureSuccessStatusCode();

                // 读取内容
                var html = await response.Content.ReadAsStringAsync();
                // 保存到文件
                SaveToFile(uri, html);
                return html;
            }
            catch (HttpRequestException ex)
            {
                // 处理HTTP错误
                Console.WriteLine($"[网络异常] {uri}\n状态码: {ex.StatusCode}");
                return null;
            }
        }

        // 从网页内容里找链接
        static void ParseAndEnqueueLinks(string html, Uri baseUri)
        {
            // 用正则找所有<a>标签的href
            var matches = Regex.Matches(html, @"<a\s+[^>]*href\s*=\s*[""']([^""'#]+)");

            // 处理每个找到的链接
            foreach (Match match in matches)
            {
                var rawUrl = match.Groups[1].Value.Trim();
                ProcessNewUrl(rawUrl, baseUri);
            }
        }

        // 处理新发现的链接
        static void ProcessNewUrl(string rawUrl, Uri baseUri)
        {
            try
            {
                // 把相对路径转成完整网址
                if (Uri.TryCreate(baseUri, rawUrl, out Uri newUri))
                {
                    // 检查是否合法
                    if (IsValidUrl(newUri) &&
                        // 确保是新的网址
                        ProcessedUrls.TryAdd(newUri, false))
                    {
                        // 加入待处理队列
                        UrlQueue.Add(newUri);
                        Console.WriteLine($"发现新网址: {newUri}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"处理链接时出错: {rawUrl}\n原因: {ex.Message}");
            }
        }

        // 检查网址是否合法
        static bool IsValidUrl(Uri uri)
        {
            // 只处理http和https
            return uri.Scheme == Uri.UriSchemeHttp ||
                   uri.Scheme == Uri.UriSchemeHttps;
        }

        // 保存内容到文件
        static void SaveToFile(Uri uri, string content)
        {
            // 生成安全的文件名：日期_域名.html
            var safeName = $"{DateTime.Now:yyyyMMddHHmmss}_{uri.Host}";
            // 替换掉特殊字符
            safeName = Regex.Replace(safeName, "[^a-zA-Z0-9]", "_") + ".html";

            try
            {
                // 写入文件
                File.WriteAllText(
                    Path.Combine(SaveDirectory, safeName),
                    content,
                    Encoding.UTF8
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"保存文件出错: {safeName}\n原因: {ex.Message}");
            }
        }
    }
}