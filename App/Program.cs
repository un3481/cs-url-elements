using System;
using PuppeteerSharp;

namespace App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("missing parameter 'url'");
                return;
            };
            var url = args[0];
            try
            {
                // Download Browser
                await new BrowserFetcher().DownloadAsync();
                var browser = await Puppeteer.LaunchAsync(GetOptions());
                // Get Page Content
                var page = await browser.NewPageAsync();
                await page.GoToAsync(url);
                // Wait until no more resources are loaded
                await page.WaitForTimeoutAsync(3000);
                // Get All Elements
                var content = await page.GetContentAsync();
                // Close Browser
                await browser.CloseAsync();
                // Write to File
                var path = DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".html";
                File.WriteAllText(path, content);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static LaunchOptions GetOptions()
        {
            return new LaunchOptions
            {
                ExecutablePath = "/usr/bin/chromium-browser",
                Headless = false,
                LogProcess = false,
                Devtools = false,
                IgnoreHTTPSErrors = true,
                DumpIO = false,
                Timeout = 0,
                Args = new []
                {
                    "--disable-web-security",
                    "--disable-background-networking",
                    "--disable-background-timer-throttling",
                    "--disable-backgrounding-occluded-windows",
                    "--disable-breakpad",
                    "--disable-client-side-phishing-detection",
                    "--disable-default-apps",
                    "--disable-dev-shm-usage",
                    "--disable-extensions",
                    "--disable-features=site-per-process",
                    "--disable-hang-monitor",
                    "--disable-ipc-flooding-protection",
                    "--disable-popup-blocking",
                    "--disable-prompt-on-repost",
                    "--disable-renderer-backgrounding",
                    "--disable-sync",
                    "--disable-translate",
                    "--metrics-recording-only",
                    "--safebrowsing-disable-auto-update",
                    "--mute-audio",
                    "--disable-gpu",
                    "--disk-cache-dir=\"C:\\ProgramData\\PuppeteerService\\Cache\"",
                    "--enable-accelerated-mjpeg-decode",
                    "--enable-accelerated-video",
                    "--enable-gpu-rasterization",
                    "--no-sandbox",
                    "--disable-setuid-sandbox",
                    "--enable-native-gpu-memory-buffers",
                    "--ignore-gpu-blacklist"
                }
            };
        }
    }
}