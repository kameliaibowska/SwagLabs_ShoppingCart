using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class ProductsListPageTests : BaseTest, Constants
    {
        private ProductsListPage page;
        private ShoppingCartIconPage shoppingCartIconPage;

        public ProductsListPageTests(string username, string password) : base(username, password)
        {
        }

        [SetUp]
        public new void Setup()
        {
            shoppingCartIconPage = new ShoppingCartIconPage(driver);
            page = new ProductsListPage(driver);
        }

        [Test]
        public async Task CheckProductsContentIsNotEmptyAsync()
        {

            var products = await page.GetProductsCountAsync();
            Assert.That(products, Is.GreaterThan(0));

            var i = 0;

            foreach (var product in page.GetProductsList())
            {
                Assert.Multiple(() =>
                {
                    Assert.That(string.IsNullOrWhiteSpace(product.ProductTitle), Is.False);
                    Assert.That(string.IsNullOrWhiteSpace(product.ProductDescription), Is.False);
                    Assert.That(product.ProductPrice > 0, Is.True);
                    Assert.That(page.GetProductLink(i), Is.Not.Empty);
                });

                i++;
            }
        }

        [TestCase(Constants.PriceLowToHigh)]
        [TestCase(Constants.PriceHighToLow)]
        public async Task OrderProductsByPriceAsync(string option)
        {
            await page.SortProductsAsync(option);
            var productsPrices = await page.DecimalPricesAsync();

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
        public async Task OrderProductsByNameZtoAAsync(string option)
        {
            await page.SortProductsAsync(option);
            var productsNames = await page.ProductsNamesAsync();

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
        public async Task AddFirstProductToShoppingCartAsync()
        {
            await Task.Run(async () =>
            {
                await page.SortProductsAsync(Constants.PriceLowToHigh);
                await page.AddRemoveProductAsync();
            });

            var shoppingCartItems = shoppingCartIconPage.GetShoppingCartItems();

            Assert.That(shoppingCartItems, Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveProductFromShoppingCartAsync()
        {
            await page.SortProductsAsync(Constants.NameAtoZ);
            await page.AddRemoveProductAsync();

            var shoppingCartItems = shoppingCartIconPage.GetShoppingCartItems();

            Assert.That(shoppingCartItems, Is.EqualTo(1));

            await page.AddRemoveProductAsync();

            var cartContent = shoppingCartIconPage.VerifyShoppingCartIsEmpty();

            Assert.That(cartContent, Is.EqualTo(0));
        }
    }
}
