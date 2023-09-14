
using SwagLabs_ShoppingCart.Models.DTO;
using SwagLabs_ShoppingCart.Pages;
using TechTalk.SpecFlow;

namespace SwagLabs_ShoppingCart.StepDefinitions
{
    [Binding]
    public class CheckoutOverviewSteps : BaseSteps
    {
        private ProductsListPage productsPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private ShoppingCartPage shoppingCartPage;
        private CheckoutOverviewPage checkoutOverviewPage;
        private CheckoutInformationPage checkoutInformationPage;
        private Product firstSelectedProductFromList;
        private Product secondSelectedProductFromList;

        [BeforeScenario]
        public void BeforeScenario()
        {
            productsPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);
            checkoutOverviewPage = new CheckoutOverviewPage(driver);
            checkoutInformationPage = new CheckoutInformationPage(driver);
        }

        [AfterScenario]
        public void AfterScenarioAsync()
        {
            driver.Quit();
        }

        [Given(@"I login to examine checkout overview page")]
        public void GivenLoginSuccessfullyWithValidCredentials()
        {
            LoginWithValidCredentials();
        }

        [When(@"I add product and checkout it")]
        public void WhenIAddProductAndCheckout()
        {
            productsPage.SortProducts(Constants.PriceLowToHigh);

            // add first product from the list by price
            productsPage.AddRemoveProduct();
            firstSelectedProductFromList = productsPage.GetProductListElements();

            // add second product from the list by name
            productsPage.SortProducts(Constants.NameZtoA);
            productsPage.AddRemoveProduct();
            secondSelectedProductFromList = productsPage.GetProductListElements();

            shoppingCartIconPage.GoToShoppingCart();
            shoppingCartPage.Checkout();
            checkoutInformationPage.FieldOutUserInformation(Constants.CheckoutInformationFirstName,
                Constants.CheckoutInformationLastName,
                Constants.CheckoutInformationZipCode);
            checkoutInformationPage.ContinueCheckout();

            Assert.That(checkoutOverviewPage.IsPageOpen(), Is.True,
                Constants.PageNotFound);
            Assert.That(checkoutOverviewPage.CheckPageTitle(), Is.EqualTo(Constants.CheckoutOverviewTitle),
                Constants.IncorrectPageTitle);
        }

        [Then(@"Checkout overview page is loaded with correct content")]
        public void ThenCheckoutOverviewPageIsLoadedWithCorrectContent()
        {
            var checkoutOverviewItemsCount = checkoutOverviewPage.GetCheckoutOverviewItemsCount();

            var checkoutOverviewItems = checkoutOverviewPage.GetCheckoutOverviewProducts();
            var expectedItems = new List<Product>
            {
                firstSelectedProductFromList,
                secondSelectedProductFromList
            };

            Assert.Multiple(() =>
            {
                Assert.That(checkoutOverviewItems.SequenceEqual(expectedItems), Is.True);
                Assert.That(shoppingCartIconPage.GetShoppingCartItems(),
                    Is.EqualTo(checkoutOverviewItemsCount));
                Assert.That(checkoutOverviewPage.IsPaymentInformationLabelDisplayed());
                Assert.That(checkoutOverviewPage.GetPaymentInformation(), Is.EqualTo(Constants.SauceCard));
                Assert.That(checkoutOverviewPage.IsShippingInformationLabelDisplayed());
                Assert.That(checkoutOverviewPage.GetShippingInformation(), Is.EqualTo(Constants.SauceShoppingInformation));
                Assert.That(checkoutOverviewPage.IsPriceTotalLabelDisplayed());
            });

            var itemTotalValue = checkoutOverviewPage.GetItemTotalValue();
            var productsPricesSum = checkoutOverviewItems.Sum(p => p.ProductPrice);
            var taxValue = Math.Round(checkoutOverviewPage.GetTaxValue(), 2);
            var taxOverTheSum = Math.Round(productsPricesSum * 0.08, 2);
            var totalValue = checkoutOverviewPage.GetTotalValue();
            var sum = Math.Round(itemTotalValue + taxValue, 2);

            Assert.Multiple(() =>
            {
                Assert.That(itemTotalValue, Is.EqualTo(productsPricesSum));
                Assert.That(taxValue.CompareTo(taxOverTheSum), Is.EqualTo(0));
                Assert.That(totalValue, Is.EqualTo(itemTotalValue + taxValue));
            });
        }
    }
}
