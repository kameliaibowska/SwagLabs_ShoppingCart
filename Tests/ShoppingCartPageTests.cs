using SwagLabs_ShoppingCart.Models.DTO;
using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests 
{
    public class ShoppingCartPageTests : BaseTest, Constants
    {
        private ShoppingCartPage page;
        private LoginPage loginPage;
        private ProductsListPage productsListPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private Product firstSelectedProductFromList;
        private Product secondSelectedProductFromList;

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage(driver);
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            page = new ShoppingCartPage(driver);

            loginPage.Open();
            loginPage.Login(Constants.ValidUsername, Constants.ValidPassword);
            NavigateToShoppingCartPage();
        }

        [Test]
        public void CheckCartItemsCount()
        {
            var cartItems = page.GetCartItemsCount();
            var shoppingCartItemsCount = shoppingCartIconPage.GetShoppingCartItems();

            Assert.That(cartItems, Is.EqualTo(shoppingCartItemsCount));

            var shoppingCartItems = page.GetShoppingCartProducts();
            var expectedItems = new List<Product> 
            { 
                firstSelectedProductFromList,
                secondSelectedProductFromList
            };

            Assert.That(shoppingCartItems.SequenceEqual(expectedItems), Is.True);

            page.ContinueShopping();

            Assert.That(productsListPage.IsPageOpen(), Is.True);
        }

        [Test]
        public void RemoveProductsFromShoppingCart()
        {
            var cartItems = page.GetCartItemsCount();
            var shoppingCartItems = shoppingCartIconPage.GetShoppingCartItems();

            Assert.That(cartItems, Is.EqualTo(shoppingCartItems));

            page.RemoveProduct();
            shoppingCartItems = shoppingCartIconPage.GetShoppingCartItems();

            Assert.That(shoppingCartItems, Is.EqualTo(cartItems - 1));
        }

        private void NavigateToShoppingCartPage()
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

            Assert.IsTrue(page.IsPageOpen());
            Assert.That(page.CheckPageTitle(), Is.EqualTo(Constants.ShoppingCartTitle));
        }
    }
}
