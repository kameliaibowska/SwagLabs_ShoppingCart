using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class CheckoutCompletePageTests : BaseTest, Constants
    {
        private CheckoutOverviewPage checkoutOverviewPage;
        private ProductsListPage productsListPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private ShoppingCartPage shoppingCartPage;
        private CheckoutInformationPage checkoutInformationPage;
        private CheckoutCompletePage page;

        public CheckoutCompletePageTests(string username, string password) : base(username, password)
        {
        }

        [SetUp]
        public new async Task Setup()
        {
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);
            checkoutOverviewPage = new CheckoutOverviewPage(driver);
            checkoutInformationPage = new CheckoutInformationPage(driver);
            page = new CheckoutCompletePage(driver);

            await FinalizeOrderAsync();
        }

        [Test]
        public void CheckCheckoutCompleteElementsAndGoBackToProducts()
        {
            Assert.That(page.CheckCompleteImage, Is.True);
            Assert.Multiple(() =>
            {
                Assert.That(page.CheckCompletePageTitle(), Is.EqualTo(Constants.CheckoutCompleteTitle));
                Assert.That(page.CheckCompleteMessage(), Is.EqualTo(Constants.CompleteMessageHeader));
                Assert.That(page.CheckCompleteDescription(), Is.EqualTo(Constants.CompleteMessageDescription));
            });

            page.GoBackToHome();
            Assert.That(productsListPage.IsPageOpen(), Is.True,
                Constants.PageNotFound);
        }

        private async Task FinalizeOrderAsync()
        {
            await productsListPage.SortProductsAsync(Constants.PriceLowToHigh);

            // add first product from the list by price
            await productsListPage.AddRemoveProductAsync();
            await productsListPage.SortProductsAsync(Constants.NameZtoA);

            // add second product from the list by name
            await productsListPage.AddRemoveProductAsync();
            await shoppingCartIconPage.GoToShoppingCartAsync();
            await shoppingCartPage.CheckoutAsync();
            await checkoutInformationPage.FieldOutUserInformationAsync(
                Constants.CheckoutInformationFirstName,
                Constants.CheckoutInformationLastName,
                Constants.CheckoutInformationZipCode);
            await checkoutInformationPage.ContinueCheckoutAsync();
            await checkoutOverviewPage.FinishOrderAsync();

            Assert.That(page.IsPageOpen(), Is.True,
                Constants.PageNotFound);
        }
    }
}
