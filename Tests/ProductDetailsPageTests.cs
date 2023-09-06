using SwagLabs_ShoppingCart.Models.DTO;
using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class ProductDetailsPageTests : BaseTest, Constants
    {
        private ProductsListPage productsListPage;
        private LoginPage loginPage;
        private ProductDetailsPage page;
        private Product selectedProductFromList;
        private ShoppingCartIconPage shoppingCartIconPage;

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage(driver);
            productsListPage = new ProductsListPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            loginPage.Open();
            loginPage.Login(Constants.ValidUsername, Constants.ValidPassword);
            OpenProductDetailsPage();
        }

        [Test]
        public void CheckProductDetailsContent()
        {
            var product = page.GetProductElements();
            Assert.That(product, Is.Not.Null);

            Assert.That(selectedProductFromList.ProductTitle, Is.EqualTo(product.ProductTitle));
            Assert.That(selectedProductFromList.ProductDescription, Is.EqualTo(product.ProductDescription));
            Assert.That(selectedProductFromList.ProductPrice, Is.EqualTo(product.ProductPrice));

            page.BackToProductsList();

            Assert.IsTrue(productsListPage.IsPageOpen());
        }

        [Test]
        public void AddToCartProduct()
        {
            var product = page.GetProductElements();
            Assert.That(product, Is.Not.Null);

            page.PressAddToCartRemoveButton();
            Assert.That(shoppingCartIconPage.GetShoppingCartItems, Is.EqualTo(1));

            page.PressAddToCartRemoveButton();
            Assert.That(shoppingCartIconPage.VerifyShoppingCartIsEmpty, Is.EqualTo(0));
        }

        private void OpenProductDetailsPage()
        {
            productsListPage.SortProducts(Constants.PriceLowToHigh);
            var firstProduct = productsListPage.GetProductContent().First();
            selectedProductFromList = productsListPage.GetProductElements(firstProduct);
            var productLink = productsListPage.GetProductLinkId(firstProduct);
            var productId = productsListPage.GetProductId(productLink);
            productsListPage.ClickFirstProductTitle(productId);
            page = new ProductDetailsPage(driver, productId);

            Assert.That(driver.Url, Is.EqualTo(page.GetProductUrl()));
        }
    }
}
