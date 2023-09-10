using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Models
{
    public class ProductDetailsPageElements : BasePage
    {
        protected int i;

        public ProductDetailsPageElements(IWebDriver driver, int i) : base(driver)
        {
            this.i = i;
        }

        protected string ItemUrl()
        {
            var url = $"{Constants.BaseUrl}/inventory-item.html?id={i}";
            driver.Navigate().GoToUrl(url);
            return url;
        }
        protected IWebElement BackToProductsButton => driver.FindElement(By.Id("back-to-products"));

        protected IWebElement AddToCartButton => driver.FindElement(By.CssSelector(".btn_inventory"));
    }
}
