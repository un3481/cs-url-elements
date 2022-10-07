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
                await new BrowserFetcher().DownloadAsync();
                var browser = await Puppeteer.LaunchAsync(GetOptions());
                var page = await browser.NewPageAsync();
                await page.GoToAsync(url);
                await page.ScreenshotAsync("print.jpg");

                Console.WriteLine(url);
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
                Headless = false,
                Timeout = 0,
                Args = new string[] {
                    "--no-sandbox",
                    "--disable-notifications",
                    "--disable-dev-shm-usage",
                    "--disable-setuid-sandbox",
                    "--lang=en-US,en-GB,en",
                },
                Devtools = false,
                IgnoreHTTPSErrors = true,
                DumpIO = false
            };
        }
    }
}