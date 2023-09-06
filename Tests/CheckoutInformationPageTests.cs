using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class CheckoutInformationPageTests : BaseTest, Constants
    {
        private CheckoutInformationPage page;
        private LoginPage loginPage;
        private ProductsListPage productsListPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private ShoppingCartPage shoppingCartPage;
        private CheckoutOverviewPage checkoutOverviewPage;

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage(driver);
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);
            page = new CheckoutInformationPage(driver);
            checkoutOverviewPage = new CheckoutOverviewPage(driver);

            loginPage.Open();
            loginPage.Login(Constants.ValidUsername, Constants.ValidPassword);
            NavigateToCheckoutInformationPage();
        }

        [TestCase(Constants.CheckoutInformationFirstName, 
            Constants.CheckoutInformationLastName, 
            Constants.CheckoutInformationZipCode)]
        [TestCase("", Constants.CheckoutInformationLastName, Constants.CheckoutInformationZipCode)]
        [TestCase(Constants.CheckoutInformationFirstName, "", Constants.CheckoutInformationZipCode)]
        [TestCase(Constants.CheckoutInformationFirstName, Constants.CheckoutInformationLastName, "")]
        public void FillCheckoutInformationFields(string firstName, string lastName, string zipCode)
        {
            page.FieldOutUserInformation(firstName, lastName, zipCode);
            page.ContinueCheckout();

            if (firstName.Length <= 0)
            {
                Assert.True(page.CheckoutInformationErrorMessageExist());
                Assert.That(page.GetCheckoutInformationErrorText(),
                    Is.EqualTo(Constants.CheckoutInformationFirstNameErrorMessage));
            }
            else if (lastName.Length <= 0)
            {
                Assert.True(page.CheckoutInformationErrorMessageExist());
                Assert.That(page.GetCheckoutInformationErrorText(),
                    Is.EqualTo(Constants.CheckoutInformationLastNameErrorMessage));
            }
            else if (zipCode.Length <= 0)
            {
                Assert.True(page.CheckoutInformationErrorMessageExist());
                Assert.That(page.GetCheckoutInformationErrorText(),
                    Is.EqualTo(Constants.CheckoutInformationZipCodeErrorMessage));
            }
            else 
            {
                Assert.IsTrue(checkoutOverviewPage.IsPageOpen());
            }
        }

        private void NavigateToCheckoutInformationPage()
        {
            productsListPage.SortProducts(Constants.PriceLowToHigh);

            // add first product from the list by price
            productsListPage.AddRemoveProduct();
            productsListPage.SortProducts(Constants.NameZtoA);

            // add first product from the list by name
            productsListPage.AddRemoveProduct();
            shoppingCartIconPage.GoToShoppingCart();
            shoppingCartPage.Checkout();

            Assert.IsTrue(page.IsPageOpen());
            Assert.That(page.CheckPageTitle(), Is.EqualTo(Constants.CheckoutInformationTitle));
        }
    }
}
