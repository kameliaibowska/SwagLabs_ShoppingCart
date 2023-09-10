using SwagLabs_ShoppingCart.Models.DTO;
using SwagLabs_ShoppingCart.Pages;
using TechTalk.SpecFlow;

namespace SwagLabs_ShoppingCart.StepDefinitions
{
    [Binding]
    public class ProductDetailsSteps : BaseSteps
    {
        private ProductsListPage? productsPage;
        private ProductDetailsPage? productDetailsPage;
        private Product? selectedProductFromList;

        [BeforeScenario]
        public void BeforeScenario()
        {
            productsPage = new ProductsListPage(driver);
        }

        [AfterScenario]
        public void AfterScenarioAsync()
        {
            driver.Quit();
        }

        [Given(@"I login to examine product details page")]
        public void GivenLoginSuccessfullyWithValidCredentials()
        {
            LoginWithValidCredentials();
        }

        [When(@"I select product in products page")]
        public void WhenISelectProduct()
        {
            productsPage.SortProducts(Constants.PriceLowToHigh);
            selectedProductFromList = productsPage.GetProductListElements();
            var productLink = productsPage.GetProductLinkId();
            var productId = productsPage.GetProductId(productLink);
            productsPage.ClickFirstProductTitle(productId);
            productDetailsPage = new ProductDetailsPage(driver, productId);
        }

        [Then(@"Product details page is loaded with correct content")]
        public void ThenProductDetailsPageIsLoadedWithCorrectContent()
        {
            var product = productDetailsPage.GetProductElements();
            Assert.That(product, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(selectedProductFromList?.ProductTitle, Is.EqualTo(product.ProductTitle));
                Assert.That(selectedProductFromList?.ProductDescription, Is.EqualTo(product.ProductDescription));
                Assert.That(selectedProductFromList?.ProductPrice, Is.EqualTo(product.ProductPrice));
            });
            productDetailsPage.BackToProductsList();

            Assert.That(productsPage.IsPageOpen(), Is.True,
                Constants.PageNotFound);
        }
    }
}
