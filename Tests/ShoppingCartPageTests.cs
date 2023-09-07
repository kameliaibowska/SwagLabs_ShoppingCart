using SwagLabs_ShoppingCart.Models.DTO;
using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class ShoppingCartPageTests : BaseTest, Constants
    {
        private ShoppingCartPage page;
        private ProductsListPage productsListPage;
        private ShoppingCartIconPage shoppingCartIconPage;
        private Product firstSelectedProductFromList;
        private Product secondSelectedProductFromList;

        public ShoppingCartPageTests(string username, string password) : base(username, password)
        {
        }

        [SetUp]
        public new async Task Setup()
        {
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            page = new ShoppingCartPage(driver);

            await NavigateToShoppingCartPageAsync();
        }

        [Test]
        public async Task CheckCartItemsCountAsync()
        {
            var cartItems = page.GetCartItemsCount();
            var shoppingCartItemsCount =  shoppingCartIconPage.GetShoppingCartItems();

            Assert.That(cartItems, Is.EqualTo(shoppingCartItemsCount),
                Constants.IncorrectShoppingCartCount);

            var shoppingCartItems = page.GetShoppingCartProducts();
            var expectedItems = new List<Product>
            {
                firstSelectedProductFromList,
                secondSelectedProductFromList
            };

            Assert.That(shoppingCartItems.SequenceEqual(expectedItems), Is.True,
                Constants.ProductsAreNotSame);

            await page.ContinueShoppingAsync();

            Assert.That(productsListPage.IsPageOpen(), Is.True,
                Constants.PageNotFound);
        }

        [Test]
        public async Task RemoveProductsFromShoppingCart()
        {
            var cartItems = page.GetCartItemsCount();
            var shoppingCartItems = shoppingCartIconPage.GetShoppingCartItems();

            Assert.That(cartItems, Is.EqualTo(shoppingCartItems),
                Constants.IncorrectShoppingCartCount);

            await page.RemoveProductAsync();
            shoppingCartItems = shoppingCartIconPage.GetShoppingCartItems();

            Assert.That(shoppingCartItems, Is.EqualTo(cartItems - 1),
                Constants.IncorrectShoppingCartCount);
        }

        private async Task NavigateToShoppingCartPageAsync()
        {
            await productsListPage.SortProductsAsync(Constants.PriceLowToHigh);

            // add first product from the list by price
            await productsListPage.AddRemoveProductAsync();
            firstSelectedProductFromList = productsListPage.GetProductListElements();

            // add first product from the list by name
            await productsListPage.SortProductsAsync(Constants.NameZtoA);
            await productsListPage.AddRemoveProductAsync();
            secondSelectedProductFromList =  productsListPage.GetProductListElements();

            await shoppingCartIconPage.GoToShoppingCartAsync();

            Assert.That(page.IsPageOpen(), Is.True,
                Constants.PageNotFound);
            Assert.That(page.CheckPageTitle(), Is.EqualTo(Constants.ShoppingCartTitle),
                Constants.IncorrectPageTitle);
        }
    }
}
