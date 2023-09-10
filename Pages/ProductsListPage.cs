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

        public Task<int> GetProductsCountAsync()
        {
            int count = ProductsCount.Count();
            return Task.FromResult(count);
        }

        public Product GetProductListElements()
        {
            return genericPage.GetProductElements(GetProductContent().First());
        }

        public IList<Product> GetProductsList()
        {
            var products = new List<Product>();

            foreach (var productContent in GetProductContent())
            {
                var product = GetProductListElements(productContent);
                products.Add(product);
            }

            return products;
        }

        public void SortProducts(string option)
        {
            SortProductsList(option).Click();
        }

        public async Task SortProductsAsync(string option)
        {
            await Task.Run(() =>
            {
                SortProducts(option);
            });
        }

        public List<decimal> DecimalPrices()
        {
            List<decimal> prices = ProductPricesList.Select(e => Decimal.Parse(e.Text.Trim('$'))).ToList();
            return prices;
        }

        public async Task<List<decimal>> DecimalPricesAsync()
        {
            return await Task.Run(() =>
            {
                return DecimalPrices();
            });
        }

        public List<string> ProductsNames()
        {
            List<string> names = ProductNames.Select(e => e.Text).ToList();
            return names;
        }

        public async Task<List<string>> ProductsNamesAsync()
        {
            return await Task.Run(() =>
            {
                return ProductsNames();
            });
        }

        public void AddRemoveProduct()
        {
            AddToCartButton.Click();
        }

        public async Task AddRemoveProductAsync()
        {
            await Task.Run(() =>
            {
                AddRemoveProduct();
            });
        }

        public int GetProductId(string linkId)
        {
            var id = linkId.Replace("item_", "").Replace("_title_link", "").Trim();
            return int.Parse(id);
        }

        public async Task<int> GetProductIdAsync(string linkId)
        {
            return await Task.Run(() =>
            {
                return GetProductId(linkId);
            });
        }

        public string GetProductLink(int id)
        {
            return ProductLink(id).Text;
        }

        public string GetProductLinkId()
        {
            return ProductLinkId(GetProductContent().First());
        }

        public void ClickFirstProductTitle(int i)
        {
            ProductLink(i).Click();
        }

        public GenericPage GetGenericPage()
        {
            return genericPage;
        }

        private Product GetProductListElements(IWebElement element)
        {
            return genericPage.GetProductElements(element);
        }

        private IList<IWebElement> GetProductContent()
        {
            return ProductsContent;
        }
    }
}
