namespace PlaywrightTests.Steps
{

    using HTMLReport.Drivers;
    using HTMLReport.Page.GiftCard;
    using Microsoft.Playwright;
    using NUnit.Framework;
    using System;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using TechTalk.SpecFlow.Assist;
    using HTMLReport.Page;

    [Binding]
    public class GiftCardsValidationSteps
    {

        private readonly HomePage homePage = null;
        private readonly GiftCardPage giftCardPage = null;


        public GiftCardsValidationSteps(Driver driver)
        {
            this.homePage = new HomePage(driver.Page);
            this.giftCardPage = new GiftCardPage(driver.Page);
          
        }

        [Given(@"I navigate to '([^']*)'")]
        public async Task NavigateTo(string url)
        {
           await this.homePage.NavigatePage(url);
           await this.homePage.CheckTitleAsync(url);
           Assert.IsTrue(await homePage.IsElementVisible(HomePage.AmazonLogo));
        }

        [Then(@"I search '([^']*)'")]
        public async Task Search(string searchText)
        {
            await this.homePage.Search(searchText);

        }

        [Then(@"I choose the gift card by type name '([^']*)'")]
        public async Task ClickOnGiftCardByTypeName(string giftCardTypeName)
        {
            await this.homePage.ClickToGifftCards();
        }

        [Then(@"I wait load page state '([^']*)'")]
        public async Task WaitLoadState(string stateToWaite)
        {
            Enum.TryParse(stateToWaite, out LoadState state);

            await this.homePage.GetPage().WaitForLoadStateAsync(state);

        }

        [Then(@"I enter gift card details")]
        public async Task EnterGiftCardDetails(Table details)
        {
            var cardDetails = details.CreateSet<GiftCardDAO>();
            if(cardDetails == null)
            {
                this.homePage.StopTestWithReason("EnterGiftCardDetails::cardDetails == null");
                return;
            }
            foreach (var item in cardDetails)
            {
                int amountValue = 0;
                if (item.Amount != null)
                {
                    await giftCardPage.InputAmount(item.Amount);
                    int.TryParse(item.CustomAmount, out amountValue);
                }
                if(item.DeliveryEmail != null)
                {
                    await giftCardPage.ClickToEmailButton();
                    await giftCardPage.InputToEmailGiftCard(item.DeliveryEmail);
                }
               if(item.From  != null)
                {
                    await giftCardPage.InputFromGiftCard(item.From);
                }
                if (item.Message != null)
                {
                    await giftCardPage.InputMessageGiftCard(item.Message);
                }
                if(item.Quantity != null)
                {
                    await giftCardPage.InputQuantityGiftCard(item.Quantity); 
                }
                if(item.DeliveryDate != null)
                {
                    await giftCardPage.ChooseDateGiftCard(item.DeliveryDate);
                }
                
            }
        }

        [Then(@"I click to button by name '([^']*)'")]
        public async Task ClickToButtonByName(string buttonName)
        {
            await giftCardPage.ClickToButtonByName(buttonName);
        }

        [Then(@"I validate cart total amount")]
        public async Task ValidateCartTotalAmmount()
        {
            await giftCardPage.ValidateAddSuccess("Added to Cart");
        }
    }
}
