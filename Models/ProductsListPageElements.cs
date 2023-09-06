using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Models
{
    public class ProductsListPageElements : BasePage
    {
        public ProductsListPageElements(IWebDriver driver) : base(driver)
        {
        }

        protected override string BaseUrl => "https://www.saucedemo.com/inventory.html";

        protected IList<IWebElement> ProductsCount => driver.FindElements(By.ClassName("inventory_item"));

        protected IWebElement AddToCartButton => driver.FindElement(By.CssSelector(".inventory_item:nth-child(1) .btn_inventory"));

        protected IList<IWebElement> ProductsContent
        {
            get
            {
                return driver.FindElements(By.ClassName("inventory_item_description")).ToList();
            }
        }

        protected string ProductLinkId(IWebElement productElement)
        {
            return productElement.FindElement(By.TagName("a")).GetAttribute("id");
        }

        protected IList<IWebElement> ProductPricesList => driver.FindElements(By.CssSelector(".inventory_item_price"));

        protected IList<IWebElement> ProductNames => driver.FindElements(By.CssSelector(".inventory_item_name"));

        protected IWebElement SortProductsList(string option)
        {
            return driver.FindElement(By.CssSelector($".product_sort_container option[value='{option}']"));
        }

        protected IWebElement ProductLink(int id)
        {
            return driver.FindElement(By.Id($"item_{id}_title_link"));
        }
    }
}
