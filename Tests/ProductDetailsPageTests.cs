using SwagLabs_ShoppingCart.Models.DTO;
using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class ProductDetailsPageTests : BaseTest, Constants
    {
        private ProductsListPage productsListPage;
        private ProductDetailsPage? page;
        private Product? selectedProductFromList;
        private ShoppingCartIconPage shoppingCartIconPage;

        public ProductDetailsPageTests(string username, string password) : base(username, password)
        {
        }

        [SetUp]
        public new async Task Setup()
        {
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);

            await OpenProductDetailsPageAsync();
        }

        [Test]
        public void CheckProductDetailsContent()
        {
            var product = page.GetProductElements();
            Assert.That(product, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(selectedProductFromList?.ProductTitle, Is.EqualTo(product.ProductTitle));
                Assert.That(selectedProductFromList?.ProductDescription, Is.EqualTo(product.ProductDescription));
                Assert.That(selectedProductFromList?.ProductPrice, Is.EqualTo(product.ProductPrice));
            });
            page.BackToProductsList();

            Assert.That(productsListPage.IsPageOpen(), Is.True,
                Constants.PageNotFound);
        }

        [Test]
        public void AddToCartProduct()
        {
            var product = page.GetProductElements();
            Assert.That(product, Is.Not.Null,
                Constants.MissingProducts);

            page.PressAddToCartRemoveButton();
            Assert.That(shoppingCartIconPage.GetShoppingCartItems, Is.EqualTo(1),
                Constants.IncorrectShoppingCartCount);

            page.PressAddToCartRemoveButton();
            Assert.That(shoppingCartIconPage.VerifyShoppingCartIsEmpty, Is.EqualTo(0),
                Constants.ShoppingCartIsNotEmpty);
        }

        private async Task OpenProductDetailsPageAsync()
        {
            await productsListPage.SortProductsAsync(Constants.PriceLowToHigh);
            selectedProductFromList = productsListPage.GetProductListElements();
            var productLink = productsListPage.GetProductLinkId();
            var productId = await productsListPage.GetProductIdAsync(productLink);
            productsListPage.ClickFirstProductTitle(productId);
            page = new ProductDetailsPage(driver, productId);

            Assert.That(driver.Url, Is.EqualTo(page.GetProductUrl()),
                Constants.IncorrectUrl);
        }
    }
}
