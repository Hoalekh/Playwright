using HTMLReport.Reporter;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HTMLReport.page
{
    public class BasePage
    {

        private readonly IPage page;

        public BasePage(IPage page) => this.page = page;

        public static string EnterbuttonName => "Enter";

        public IPage GetPage() => this.page;
        public async Task ClickToButtonByName(string buttonName)
        {
            await this.GetPage().GetByRole(AriaRole.Button, new PageGetByRoleOptions() { Name = buttonName, Exact = true }).ClickAsync();

        }

        public async Task PressEnterButton(string placeholder)
        {
            await this.GetPage().GetByPlaceholder(placeholder).PressAsync(EnterbuttonName);
        }

        public async Task InputTextByLabel(string locator, string value)
        {
            try
            {
                await this.GetPage().GetByLabel(locator).ClickAsync();
                await this.GetPage().GetByLabel(locator).FillAsync(value);

                HtmlReport.Pass("Input Text success: " + value);
            }
            catch (Exception ex)
            {
                throw ex;
                HtmlReport.Fail("Input text unsuccess: " + value, await TakeScreenshot());
            }

        }

        public async Task InputTextByPlaceholder(string locator, string value)
        {
            try
            {
                await this.GetPage().GetByPlaceholder(locator).FillAsync(value);

                HtmlReport.Pass("Input Text success: " + value);
            }
            catch (Exception ex)
            {
                throw ex;
                HtmlReport.Fail("Input text unsuccess: " + value, await TakeScreenshot());
            }
        }

        public async Task InputTextByLocator(string locator, string value)
        {
            try
            {
                await this.GetPage().Locator(locator).FillAsync(value);

                HtmlReport.Pass("Input Text success: " + value);
            }
            catch (Exception ex)
            {
                throw ex;
                HtmlReport.Fail("Input text unsuccess: " + value, await TakeScreenshot());
            }

        }

        public async Task<string> TakeScreenshot()
        {
            string path = HtmlReportDirectory.SCREENSHOT_PATH + ("/screenshot_" + DateTime.Now.ToString("yyyyMMdd")) + ".png";
            await page.ScreenshotAsync(new PageScreenshotOptions
            {
                Path = path
            });
            return path;
        }

        public async Task<string?> GetText(string locator)
        {
            try
            {
                var element = await this.GetPage().QuerySelectorAsync(locator);
                var text = await element.TextContentAsync();
                return text;
                await TakeScreenshot();
                HtmlReport.Pass("Text is correct: " + locator);
            }
            catch (Exception ex)
            {
                throw ex;
                HtmlReport.Fail("Text is not correct: " + locator, await TakeScreenshot());
            }


        }
        public async Task CheckTitleAsync(string url)
        {
            try
            {
                string title = url.Substring(url.IndexOf(".") + 1);
                string removeLastCharFromTitleResult = title.Remove(title.Length - 1, 1);

                removeLastCharFromTitleResult.ToUpper();
                string titleToValidate = removeLastCharFromTitleResult.Remove(1).ToUpper() + removeLastCharFromTitleResult.Substring(1);

                // validate URL link name
                await Assertions.Expect(this.GetPage()).ToHaveTitleAsync(new Regex(titleToValidate));

                HtmlReport.Pass("Title is correct: " + url);
            }
            catch (Exception ex)
            {
                throw ex;
                HtmlReport.Fail("Title is not correct: " + url, await TakeScreenshot());
            }


        }

        public async Task ChooseCalendarDate(string locatorCalendar, string date)
        {

            await this.GetPage().Locator(locatorCalendar).ClickAsync();
            await this.GetPage().GetByRole(AriaRole.Link, new() { Name = date, Exact = true }).ClickAsync();
        }

        public async Task<bool> IsElementVisible(string locator)
        {
            try
            {
                var element = await GetPage().QuerySelectorAsync(locator);
                if (element == null)
                {
                    throw new Exception($"Element with locator '{locator}' not found.");
                }
                return await element.IsVisibleAsync();

                HtmlReport.Pass("Element is visible" + locator);
            }
            catch (Exception ex)
            {
                throw ex;
                HtmlReport.Fail("Element is invisible " + locator, await TakeScreenshot());
            }



        }

        public void StopTestWithReason(string reason)
        {
            var bTesstFail = true;
            bTesstFail.Should().BeFalse($"{reason}");
        }

    }
}
