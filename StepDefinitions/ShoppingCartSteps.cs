using SwagLabs_ShoppingCart.Models.DTO;
using SwagLabs_ShoppingCart.Pages;
using TechTalk.SpecFlow;

namespace SwagLabs_ShoppingCart.StepDefinitions
{
    [Binding]
    public class ShoppingCartSteps : BaseSteps
    {
        private ProductsListPage productsPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private ShoppingCartPage shoppingCartPage;
       
        private Product firstSelectedProductFromList;
        private Product secondSelectedProductFromList;

        [BeforeScenario]
        public void BeforeScenario()
        {
            productsPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            shoppingCartPage = new ShoppingCartPage(driver);
        }

        [AfterScenario]
        public void AfterScenarioAsync()
        {
            driver.Quit();
        }

        [Given(@"I login to examine shopping cart page")]
        public void GivenLoginSuccessfullyWithValidCredentials()
        {
            LoginWithValidCredentials();
        }

        [When(@"I add product in shopping cart and navigate to it")]
        public void WhenIAddProductInShoppingCart()
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

            Assert.That(shoppingCartPage.IsPageOpen(), Is.True,
                Constants.PageNotFound);
            Assert.That(shoppingCartPage.CheckPageTitle(), Is.EqualTo(Constants.ShoppingCartTitle),
                Constants.IncorrectPageTitle);
        }

        [Then(@"Shopping cart page is loaded with correct content")]
        public void ThenShoppingCartPageIsLoadedWithCorrectContent()
        {
            var cartItems = shoppingCartPage.GetCartItemsCount();
            var shoppingCartItemsCount = shoppingCartIconPage.GetShoppingCartItems();

            Assert.That(cartItems, Is.EqualTo(shoppingCartItemsCount),
                Constants.IncorrectShoppingCartCount);

            var shoppingCartItems = shoppingCartPage.GetShoppingCartProducts();
            var expectedItems = new List<Product>
            {
                firstSelectedProductFromList,
                secondSelectedProductFromList
            };

            Assert.That(shoppingCartItems.SequenceEqual(expectedItems), Is.True,
                Constants.ProductsAreNotSame);

            shoppingCartPage.ContinueShopping();

            Assert.That(productsPage.IsPageOpen(), Is.True,
                Constants.PageNotFound);
        }
    }
}
