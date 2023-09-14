using SwagLabs_ShoppingCart.Pages;
using TechTalk.SpecFlow;

namespace SwagLabs_ShoppingCart.StepDefinitions
{
    [Binding]
    public class CheckoutInformationSteps : BaseSteps
    {
        private ProductsListPage productsPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private ShoppingCartPage shoppingCartPage;
        private CheckoutOverviewPage checkoutOverviewPage;
        private CheckoutInformationPage checkoutInformationPage;

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

        [Given(@"I login to examine checkout information page")]
        public void GivenLoginSuccessfullyWithValidCredentials()
        {
            LoginWithValidCredentials();
        }

        [When(@"I add product in shopping cart and press checkout")]
        public void WhenIAddProductInShoppingCartAndPressCheckout()
        {
            productsPage.SortProducts(Constants.PriceLowToHigh);

            // add first product from the list by price
            productsPage.AddRemoveProduct();
            productsPage.SortProducts(Constants.NameZtoA);

            // add second product from the list by name
            productsPage.AddRemoveProduct();
            shoppingCartIconPage.GoToShoppingCart();
            shoppingCartPage.Checkout();

            Assert.That(checkoutInformationPage.IsPageOpen(), Is.True,
                Constants.PageNotFound);
            Assert.That(checkoutInformationPage.CheckPageTitle(),
                Is.EqualTo(Constants.CheckoutInformationTitle),
                Constants.IncorrectPageTitle);
        }

        [Then(@"I fill ""(.*)"", ""(.*)"" and ""(.*)"" in contacts to examine result")]
        public void ThenIFillContactFormData(string firstName, string lastName, string zipCode)
        {
            checkoutInformationPage.FieldOutUserInformation(firstName, lastName, zipCode);
            checkoutInformationPage.ContinueCheckout();

            if (firstName.Length <= 0)
            {
                AssertResults(Constants.CheckoutInformationFirstNameErrorMessage);
            }
            else if (lastName.Length <= 0)
            {
                AssertResults(Constants.CheckoutInformationLastNameErrorMessage);
            }
            else if (zipCode.Length <= 0)
            {
                AssertResults(Constants.CheckoutInformationZipCodeErrorMessage);
            }
            else
            {
                Assert.That(checkoutOverviewPage.IsPageOpen(), Is.True,
                    Constants.PageNotFound);
            }
        }

        private void AssertResults(string errorMessage)
        {
            Assert.That(checkoutInformationPage.CheckoutInformationErrorMessageExist(), Is.True,
                    Constants.MissingErrorMessage);
            Assert.That(checkoutInformationPage.GetCheckoutInformationErrorText(),
                Is.EqualTo(errorMessage),
                Constants.IncorectErrorMessage);
        }
    }
}
