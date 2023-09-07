using SwagLabs_ShoppingCart.Models.DTO;
using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class CheckoutOverviewPageTests : BaseTest, Constants
    {
        private CheckoutOverviewPage page;
        private ProductsListPage productsListPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private ShoppingCartPage shoppingCartPage;
        private CheckoutInformationPage checkoutInformationPage;
        private Product firstSelectedProductFromList;
        private Product secondSelectedProductFromList;

        public CheckoutOverviewPageTests(string username, string password) : base(username, password)
        {
        }

        [SetUp]
        public new async Task Setup()
        {
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);
            page = new CheckoutOverviewPage(driver);
            checkoutInformationPage = new CheckoutInformationPage(driver);

            await NavigateToCheckoutOverviewPage();
        }

        [Test]
        public async Task CheckCheckoutOverviewContent()
        {
            var checkoutOverviewItemsCount = page.GetCheckoutOverviewItemsCount();

            var checkoutOverviewItems = page.GetCheckoutOverviewProducts();
            var expectedItems = new List<Product>
            {
                firstSelectedProductFromList,
                secondSelectedProductFromList
            };

            Assert.Multiple(async () =>
            {
                Assert.That(checkoutOverviewItems.SequenceEqual(expectedItems), Is.True);
                Assert.That(shoppingCartIconPage.GetShoppingCartItems(),
                    Is.EqualTo(checkoutOverviewItemsCount));
                Assert.That(await page.IsPaymentInformationLabelDisplayedAsync());
                Assert.That(page.GetPaymentInformation(), Is.EqualTo(Constants.SauceCard));
                Assert.That(await page.IsShippingInformationLabelDisplayedAsync());
                Assert.That(page.GetShippingInformation(), Is.EqualTo(Constants.SauceShoppingInformation));
                Assert.That(page.IsPriceTotalLabelDisplayedAsync());
            });

            var itemTotalValue = page.GetItemTotalValueAsync();
            var productsPricesSum = checkoutOverviewItems.Sum(p => p.ProductPrice);
            var taxValue = Math.Round(await page.GetTaxValueAsync(), 2);
            var taxOverTheSum = Math.Round(productsPricesSum * 0.08, 2);
            var totalValue = page.GetTotalValue();
            var sum = Math.Round(itemTotalValue + taxValue, 2);

            Assert.Multiple(() =>
            {
                Assert.That(itemTotalValue, Is.EqualTo(productsPricesSum));
                Assert.That(taxValue.CompareTo(taxOverTheSum), Is.EqualTo(0));
                Assert.That(totalValue, Is.EqualTo(itemTotalValue + taxValue));
            });
        }

        private async Task NavigateToCheckoutOverviewPage()
        {
            await Task.Run(async() =>
            {
                await productsListPage.SortProductsAsync(Constants.PriceLowToHigh);

                // add first product from the list by price
                await productsListPage.AddRemoveProductAsync();
                firstSelectedProductFromList = productsListPage.GetProductListElements();

                // add first product from the list by name
                await productsListPage.SortProductsAsync(Constants.NameZtoA);
                await productsListPage.AddRemoveProductAsync();
                secondSelectedProductFromList = productsListPage.GetProductListElements();

                await shoppingCartIconPage.GoToShoppingCartAsync();
                await shoppingCartPage.CheckoutAsync();
                await checkoutInformationPage.FieldOutUserInformationAsync(Constants.CheckoutInformationFirstName,
                    Constants.CheckoutInformationLastName,
                    Constants.CheckoutInformationZipCode);
                await checkoutInformationPage.ContinueCheckoutAsync();

                Assert.That(page.IsPageOpen(), Is.True,
                    Constants.PageNotFound);
                Assert.That(page.CheckPageTitle(), Is.EqualTo(Constants.CheckoutOverviewTitle),
                    Constants.IncorrectPageTitle);
            });
        }
    }
}
