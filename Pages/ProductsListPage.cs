using SwagLabs_ShoppingCart.Models;
using SwagLabs_ShoppingCart.Models.DTO;

namespace SwagLabs_ShoppingCart.Pages
{
    public class ProductsListPage : ProductsListPageElements
    {
        private GenericPage genericPage;

        public ProductsListPage(IWebDriver driver) : base(driver)
        {
            genericPage = new GenericPage(driver);
        }

        public int GetProductsCount()
        {
            return ProductsCount.Count();
        }

        public Product GetProductElements(IWebElement element)
        {
            return genericPage.GetProductElements(element);
        }

        public IList<IWebElement> GetProductContent()
        {
            return ProductsContent;
        }

        public void SortProducts(string option)
        {
            SortProductsList(option).Click();
        }

        public List<decimal> DecimalPrices()
        {
            List<decimal> prices = ProductPricesList.Select(e => Decimal.Parse(e.Text.Trim('$'))).ToList();

            return prices;
        }

        public List<string> ProductsNames()
        {
            List<string> names = ProductNames.Select(e => e.Text).ToList();

            return names;
        }

        public void AddRemoveProduct()
        {
            AddToCartButton.Click();
        }

        public int GetProductId(string linkId)
        {
            var id = linkId.Replace("item_", "").Replace("_title_link", "").Trim();

            return int.Parse(id);
        }

        public IWebElement GetProductLink(int id)
        {
            return ProductLink(id);
        }

        public string GetProductLinkId(IWebElement productElement)
        {
            return ProductLinkId(productElement);
        }

        public void ClickFirstProductTitle(int i)
        {
            GetProductLink(i).Click();
        }
    }
}
