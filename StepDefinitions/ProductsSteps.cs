using SwagLabs_ShoppingCart.Pages;
using TechTalk.SpecFlow;

namespace SwagLabs_ShoppingCart.StepDefinitions
{
    [Binding]
    public class ProductsSteps : BaseSteps
    {
        private ProductsListPage? productsPage;

        [BeforeScenario]
        public void BeforeScenario()
        {
            productsPage = new ProductsListPage(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }

        [Given(@"I login to examine products page")]
        public void GivenLoginSuccessfullyWithValidCredentials()
        {
            LoginWithValidCredentials();
        }

        [When(@"I select on ""(.*)"" option in Products page")]
        public void WhenISortProducts(string sortOrder)
        {
            productsPage.SortProducts(MapOptionToSort(sortOrder));
        }

        [Then(@"Products are ordered by price ""(.*)""")]
        public void ThenProductsAreOrderedByPrice(string sortOrder)
        {
            var productsPrices = productsPage.DecimalPrices();
            var option = MapOptionToSort(sortOrder);

            for (int i = 0; i < productsPrices.Count - 1; i++)
            {
                switch (option)
                {
                    case Constants.PriceLowToHigh:
                        Assert.That(productsPrices[i] <= productsPrices[i + 1], 
                            $"Products are not sorted correctly. Expected: {productsPrices[i]} <= {productsPrices[i + 1]}");
                        break;
                    case Constants.PriceHighToLow:
                        Assert.That(productsPrices[i] >= productsPrices[i + 1], 
                            $"Products are not sorted correctly. Expected: {productsPrices[i]} >= {productsPrices[i + 1]}");
                        break;
                }
            }
        }

        [Then(@"Products are ordered by name ""(.*)""")]
        public void ThenProductsAreOrderedByName(string sortOrder)
        {
            var productsNames = productsPage.ProductsNames();
            var option = MapOptionToSort(sortOrder);

            for (int i = 0; i < productsNames.Count - 1; i++)
            {
                switch (option)
                {
                    case Constants.NameAtoZ:
                        Assert.That(string.Compare(productsNames[i], productsNames[i + 1]) <= 0, 
                            $"Product names are not sorted correctly in descending order. Expected: {productsNames[i]} <= {productsNames[i + 1]}");
                        break;
                    case Constants.NameZtoA:
                        Assert.That(string.Compare(productsNames[i], productsNames[i + 1]) >= 0,
                             $"Product names are not sorted correctly. Expected: {productsNames[i]} >= {productsNames[i + 1]}");
                        break;
                }
            }
        }

        private string MapOptionToSort(string sortOption)
        {
            sortOption = sortOption.Replace("price ", "").Replace("name ", "");
            var sortValue = "";

            switch (sortOption)
            {
                case "low to high":
                    sortValue = Constants.PriceLowToHigh;
                    break;
                case "high to low":
                    sortValue = Constants.PriceHighToLow;
                    break;
                case "a to z":
                    sortValue = Constants.NameAtoZ;
                    break;
                case "z to a":
                    sortValue = Constants.NameZtoA;
                    break;
            }

            return sortValue;
        }
    }
}
