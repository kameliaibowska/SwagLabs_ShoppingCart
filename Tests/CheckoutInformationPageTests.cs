using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class CheckoutInformationPageTests : BaseTest, Constants
    {
        private CheckoutInformationPage page;
        private ProductsListPage productsListPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private ShoppingCartPage shoppingCartPage;
        private CheckoutOverviewPage checkoutOverviewPage;

        public CheckoutInformationPageTests(string username, string password) : base(username, password)
        {
        }

        [SetUp]
        public new async Task Setup()
        {
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);
            page = new CheckoutInformationPage(driver);
            checkoutOverviewPage = new CheckoutOverviewPage(driver);

            await NavigateToCheckoutInformationPageAsync();
        }

        [TestCase(Constants.CheckoutInformationFirstName, 
            Constants.CheckoutInformationLastName, 
            Constants.CheckoutInformationZipCode)]
        [TestCase("", Constants.CheckoutInformationLastName, Constants.CheckoutInformationZipCode)]
        [TestCase(Constants.CheckoutInformationFirstName, "", Constants.CheckoutInformationZipCode)]
        [TestCase(Constants.CheckoutInformationFirstName, Constants.CheckoutInformationLastName, "")]
        public async Task FillCheckoutInformationFieldsAsync(string firstName, string lastName, string zipCode)
        {
            await page.FieldOutUserInformationAsync(firstName, lastName, zipCode);
            await page.ContinueCheckoutAsync();

            if (firstName.Length <= 0)
            {
                Assert.That(await page.CheckoutInformationErrorMessageExistAsync(), Is.True,
                    Constants.MissingErrorMessage);
                Assert.That(page.GetCheckoutInformationErrorText(),
                    Is.EqualTo(Constants.CheckoutInformationFirstNameErrorMessage), 
                    Constants.IncorectErrorMessage);
            }
            else if (lastName.Length <= 0)
            {
                Assert.That(await page.CheckoutInformationErrorMessageExistAsync(), Is.True,
                    Constants.MissingErrorMessage);
                Assert.That(page.GetCheckoutInformationErrorText(),
                    Is.EqualTo(Constants.CheckoutInformationLastNameErrorMessage),
                    Constants.IncorectErrorMessage);
            }
            else if (zipCode.Length <= 0)
            {
                Assert.That(await page.CheckoutInformationErrorMessageExistAsync(), Is.True,
                    Constants.MissingErrorMessage);
                Assert.That(page.GetCheckoutInformationErrorText(),
                    Is.EqualTo(Constants.CheckoutInformationZipCodeErrorMessage),
                    Constants.IncorectErrorMessage);
            }
            else
            {
                Assert.That(checkoutOverviewPage.IsPageOpen(), Is.True,
                    Constants.PageNotFound);
            }
        }

        private async Task NavigateToCheckoutInformationPageAsync()
        {
            await productsListPage.SortProductsAsync(Constants.PriceLowToHigh);

            // add first product from the list by price
            await productsListPage.AddRemoveProductAsync();
            await productsListPage.SortProductsAsync(Constants.NameZtoA);

            // add first product from the list by name
            await productsListPage.AddRemoveProductAsync();
            await shoppingCartIconPage.GoToShoppingCartAsync();
            await shoppingCartPage.CheckoutAsync();

            Assert.That(page.IsPageOpen(), Is.True,
                Constants.PageNotFound);
            Assert.That(page.CheckPageTitle(), 
                Is.EqualTo(Constants.CheckoutInformationTitle), 
                Constants.IncorrectPageTitle);
        }
    }
}
