using SwagLabs_ShoppingCart.Models.DTO;
using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class CheckoutOverviewPageTests : BaseTest, Constants
    {
        private CheckoutOverviewPage page;
        private LoginPage loginPage;
        private ProductsListPage productsListPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private ShoppingCartPage shoppingCartPage;
        private CheckoutInformationPage checkoutInformationPage;
        private Product firstSelectedProductFromList;
        private Product secondSelectedProductFromList;

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage(driver);
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);
            page = new CheckoutOverviewPage(driver);
            checkoutInformationPage = new CheckoutInformationPage(driver);

            loginPage.Open();
            loginPage.Login(Constants.ValidUsername, Constants.ValidPassword);
            NavigateToCheckoutOverviewPage();
        }

        [Test]
        public void CheckCheckoutOverviewContent()
        {
            var checkoutOvarviewItemsCount = page.GetCheckoutOverviewItemsCount();

            var checkoutOverviewItems = page.GetCheckoutOverviewProducts();
            var expectedItems = new List<Product>
            {
                firstSelectedProductFromList,
                secondSelectedProductFromList
            };

            Assert.That(checkoutOverviewItems.SequenceEqual(expectedItems), Is.True);
            Assert.That(shoppingCartIconPage.GetShoppingCartItems, Is.EqualTo(checkoutOvarviewItemsCount));
            Assert.That(page.IsPaymentInformationLabelDisplayed);
            Assert.That(page.GetPaymentInformation(), Is.EqualTo(Constants.SauceCard));
            Assert.That(page.IsShippingInformationLabelDisplayed);
            Assert.That(page.GetShippingInformation(), Is.EqualTo(Constants.SauceShoppingInformation));
            Assert.That(page.IsPriceTotalLabelDisplayed());

            var itemTotalValue = page.GetItemTotalValue();
            var productsPricesSum = checkoutOverviewItems.Sum(p => p.ProductPrice);
            var taxValue = Math.Round(page.GetTaxValue(), 2);
            var taxOverTheSum = Math.Round(productsPricesSum * 0.08, 2);
            var totalValue = page.GetTotalValue();

            Assert.That(itemTotalValue, Is.EqualTo(productsPricesSum));
            Assert.That(taxValue.CompareTo(taxOverTheSum), Is.EqualTo(0));
            Assert.That(totalValue, Is.EqualTo(itemTotalValue + taxValue));
        }

        private void NavigateToCheckoutOverviewPage()
        {
            productsListPage.SortProducts(Constants.PriceLowToHigh);

            // add first product from the list by price
            productsListPage.AddRemoveProduct();
            var firstAddedProduct = productsListPage.GetProductContent().First();
            firstSelectedProductFromList = productsListPage.GetProductElements(firstAddedProduct);

            // add first product from the list by name
            productsListPage.SortProducts(Constants.NameZtoA);           
            productsListPage.AddRemoveProduct();
            var secondAddedProduct = productsListPage.GetProductContent().First();
            secondSelectedProductFromList = productsListPage.GetProductElements(secondAddedProduct);
            shoppingCartIconPage.GoToShoppingCart();
            shoppingCartPage.Checkout();
            checkoutInformationPage.FieldOutUserInformation(Constants.CheckoutInformationFirstName,
                Constants.CheckoutInformationLastName,
                Constants.CheckoutInformationZipCode);
            checkoutInformationPage.ContinueCheckout();

            Assert.IsTrue(page.IsPageOpen());
            Assert.That(page.CheckPageTitle(), Is.EqualTo(Constants.CheckoutOverviewTitle));
        }
    }
}
