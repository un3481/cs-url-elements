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
                // Wait until DOM is loaded then wait for more 3 seconds
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
                    "--no-sandbox",
                    "--disable-setuid-sandbox",
                    "--disable-dev-shm-usage",
                    "--disable-web-security",
                    "--disable-extensions",
                    // "--disable-background-networking",
                    "--disable-background-timer-throttling",
                    "--disable-backgrounding-occluded-windows",
                    "--disable-sync",
                    "--disable-translate",
                    "--metrics-recording-only",
                    "--safebrowsing-disable-auto-update",
                    "--disable-breakpad",
                    "--disable-client-side-phishing-detection",
                    "--disable-default-apps",
                    "--disable-features=site-per-process",
                    "--disable-hang-monitor",
                    "--disable-ipc-flooding-protection",
                    "--disable-popup-blocking",
                    "--disable-prompt-on-repost",
                    "--disable-renderer-backgrounding",
                    "--mute-audio",
                    "--disable-gpu",
                    "--enable-accelerated-mjpeg-decode",
                    "--enable-accelerated-video",
                    "--enable-gpu-rasterization",
                    "--enable-native-gpu-memory-buffers",
                    "--ignore-gpu-blacklist"
                }
            };
        }
    }
}