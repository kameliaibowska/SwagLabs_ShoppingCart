using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Models
{
    public class CheckoutCompletePageElements : BasePage
    {
        public CheckoutCompletePageElements(IWebDriver driver) : base(driver)
        {
        }

        protected override string BaseUrl => "https://www.saucedemo.com/checkout-complete.html";

        protected IWebElement CompleteImage => driver.FindElement(By.ClassName("pony_express"));

        protected IWebElement CheckoutCompletePageTitle => driver.FindElement(By.ClassName("title"));

        protected IWebElement CheckoutCompletePageHeader => driver.FindElement(By.ClassName("complete-header"));

        protected IWebElement CheckoutCompletePageText => driver.FindElement(By.ClassName("complete-text"));

        protected IWebElement BackToHomeButton => driver.FindElement(By.Id("back-to-products"));
    }
}
