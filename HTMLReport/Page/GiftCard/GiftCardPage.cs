using HTMLReport.page;
using Microsoft.Playwright;

using Microsoft.Playwright.NUnit;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTMLReport.Page.GiftCard
{
    public class GiftCardPage : BasePage
    {
        public GiftCardPage(IPage page) : base(page)
        {

        }
        public static string AmountDetail => "Amount";

        public static string EmailDeliveryButton => "Email";
        public static string ToEmailGiftCard => "Enter an email for each recipient";

        public static string FromGiftCard => "From";

        public static string MessageGiftCard => "Message";

        public static string QuantityGiftCard => "Quantity";

        public static string CalendarGiftCard => "//input[@id='gc-order-form-date-val']";

        public static string TotalCard = "//span[@class='a-price sw-subtotal-amount']";
        public static string MsgAddCartSuccess = "//div[@id='NATC_SMART_WAGON_CONF_MSG_SUCCESS']";
        public async Task InputAmount(string amount)
        {
            await this.GetPage().GetByLabel(AmountDetail).FillAsync(amount);
        }
        public async Task ClickToEmailButton()
        {
            await ClickToButtonByName(EmailDeliveryButton);

        }
        public async Task InputToEmailGiftCard(string email)
        {
            await InputTextByPlaceholder(EmailDeliveryButton, email);
        }
        public async Task InputFromGiftCard(string fromGiftCard)
        {
            await InputTextByLabel(FromGiftCard, fromGiftCard);

        }
        public async Task InputMessageGiftCard(string msg)
        {

            await this.GetPage().GetByRole(AriaRole.Textbox, new() { Name = "Message" }).FillAsync(msg);

        }
        public async Task InputQuantityGiftCard(string quantity)
        {
            await InputTextByLabel(QuantityGiftCard, quantity);

        }

        public async Task ChooseDateGiftCard(string date)
        {

            if (String.Compare(date, "today", StringComparison.OrdinalIgnoreCase) == 0)
            {
                date = DateTime.Today.Day.ToString();
            }
            await this.ChooseCalendarDate(CalendarGiftCard, date);

        }
        public async Task ValidateAddSuccess(String msg)
        {
            var locator = this.GetPage().Locator(MsgAddCartSuccess);
            bool isVisible = await this.GetPage().Locator(MsgAddCartSuccess).IsVisibleAsync();
            if (isVisible)
            {
                var actualText = await GetText(MsgAddCartSuccess);
                await Assertions.Expect(locator).ToContainTextAsync(msg);
            }

        }
    }
}