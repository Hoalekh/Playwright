using HTMLReport.page;
using HTMLReport.Reporter;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLReport.Page
{
    public class HomePage : BasePage
    {
        public HomePage(IPage page) : base(page)
        {

        }


        public static string AmazonSearchPlaceholder => "Search Amazon";

        public static string EGiftCard => "Amazon.com eGift Card";
        public static string AmazonLogo => "//a[@aria-label='Amazon']";
        public async Task NavigatePage(string url)
        {
            await this.GetPage().GotoAsync(url);
            HtmlReport.Pass("Go to Url: " + url);
        }
        public async Task Search(string searchText)
        {
            await InputTextByPlaceholder(AmazonSearchPlaceholder, searchText);
            await PressEnterButton(AmazonSearchPlaceholder);
        }
        public async Task ClickToGifftCards()
        {
            await this.GetPage().GetByRole(AriaRole.Heading, new() { Name = EGiftCard }).ClickAsync();
        }
        
    }
}
