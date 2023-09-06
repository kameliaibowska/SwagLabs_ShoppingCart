using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class ProductsListPageTests : BaseTest, Constants
    {
        private ProductsListPage page;
        private LoginPage loginPage;
        private ShoppingCartIconPage shoppingCartIconPage;

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage(driver);
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            loginPage.Open();
            loginPage.Login(Constants.ValidUsername, Constants.ValidPassword);
            page = new ProductsListPage(driver);
        }

        [Test]
        public void CheckProductsContentIsNotEmpty()
        {
            var products = page.GetProductsCount();
            Assert.That(products, Is.GreaterThan(0));

            var i = 0;

            foreach (var productContent in page.GetProductContent())
            {
                var product = page.GetProductElements(productContent);

                Assert.That(string.IsNullOrWhiteSpace(product.ProductTitle), Is.False);
                Assert.That(string.IsNullOrWhiteSpace(product.ProductDescription), Is.False);
                Assert.That(product.ProductPrice > 0, Is.True);
                Assert.That(page.GetProductLink(i).ToString(), Is.Not.Empty);
                i++;
            }
        }

        [TestCase(Constants.PriceLowToHigh)]
        [TestCase(Constants.PriceHighToLow)]
        public void OrderProductsByPrice(string option)
        {
            page.SortProducts(option);
            var productsPrices = page.DecimalPrices();

            for (int i = 0; i < productsPrices.Count - 1; i++)
            {
                if (option == Constants.PriceLowToHigh)
                {
                    Assert.That(productsPrices[i] <= productsPrices[i + 1],
                        $"Products are not sorted correctly. Expected: {productsPrices[i]} <= {productsPrices[i + 1]}");
                }
                else if (option == Constants.PriceHighToLow)
                {
                    Assert.That(productsPrices[i] >= productsPrices[i + 1],
                    $"Products are not sorted correctly. Expected: {productsPrices[i]} >= {productsPrices[i + 1]}");
                }
            }
        }

        [TestCase(Constants.NameZtoA)]
        [TestCase(Constants.NameAtoZ)]
        public void OrderProductsByNameZtoA(string option)
        {
            page.SortProducts(option);
            var productsNames = page.ProductsNames();

            for (int i = 0; i < productsNames.Count - 1; i++)
            {
                if (option == Constants.NameZtoA)
                {
                    Assert.That(string.Compare(productsNames[i], productsNames[i + 1]) >= 0,
                    $"Product names are not sorted correctly. Expected: {productsNames[i]} >= {productsNames[i + 1]}");
                }
                else if (option == Constants.NameAtoZ)
                {
                    Assert.That(string.Compare(productsNames[i], productsNames[i + 1]) <= 0,
                    $"Product names are not sorted correctly in descending order. Expected: {productsNames[i]} <= {productsNames[i + 1]}");
                }
            }
        }

        [Test]
        public void AddFirstProductToShoppingCart()
        {
            page.SortProducts(Constants.PriceLowToHigh);
            page.AddRemoveProduct();

            Assert.That(shoppingCartIconPage.GetShoppingCartItems, Is.EqualTo(1));
        }

        [Test]
        public void RemoveProductToShoppingCart()
        {
            page.SortProducts(Constants.NameAtoZ);
            page.AddRemoveProduct();

            Assert.That(shoppingCartIconPage.GetShoppingCartItems, Is.EqualTo(1));

            page.AddRemoveProduct();

            Assert.That(shoppingCartIconPage.VerifyShoppingCartIsEmpty, Is.EqualTo(0));
        }
    }
}
