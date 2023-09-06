using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class CheckoutCompletePageTests : BaseTest, Constants
    {
        private CheckoutOverviewPage checkoutOverviewPage;
        private LoginPage loginPage;
        private ProductsListPage productsListPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private ShoppingCartPage shoppingCartPage;
        private CheckoutInformationPage checkoutInformationPage;
        private CheckoutCompletePage page;

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage(driver);
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);
            checkoutOverviewPage = new CheckoutOverviewPage(driver);
            checkoutInformationPage = new CheckoutInformationPage(driver);
            page = new CheckoutCompletePage(driver);

            loginPage.Open();
            loginPage.Login(Constants.ValidUsername, Constants.ValidPassword);
            FinalizeOrder();
        }

        [Test]
        public void CheckCheckoutCompleteElementsAndGoBackToProducts()
        {
            Assert.That(page.CheckCompleteImage, Is.True);
            Assert.That(page.CheckCompletePageTitle(), Is.EqualTo(Constants.CheckoutCompleteTitle));
            Assert.That(page.CheckCompleteMessege(), Is.EqualTo(Constants.CompleteMessegeHeader));
            Assert.That(page.CheckCompleteDescription(), Is.EqualTo(Constants.CompleteMessegeDescription));

            page.GoBackToHome();
            Assert.IsTrue(productsListPage.IsPageOpen());
        }

        private void FinalizeOrder()
        {
            productsListPage.SortProducts(Constants.PriceLowToHigh);

            // add first product from the list by price
            productsListPage.AddRemoveProduct();
            productsListPage.SortProducts(Constants.NameZtoA);

            // add first product from the list by name
            productsListPage.AddRemoveProduct();
            shoppingCartIconPage.GoToShoppingCart();
            shoppingCartPage.Checkout();
            checkoutInformationPage.FieldOutUserInformation(Constants.CheckoutInformationFirstName,
                Constants.CheckoutInformationLastName,
                Constants.CheckoutInformationZipCode);
            checkoutInformationPage.ContinueCheckout();
            checkoutOverviewPage.FinishOrder();

            Assert.IsTrue(page.IsPageOpen());
        }
    }
}
