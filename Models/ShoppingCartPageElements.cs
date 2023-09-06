using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Models
{
    public class ShoppingCartPageElements : BasePage
    {
        public ShoppingCartPageElements(IWebDriver driver) : base(driver)
        {
        }

        protected override string BaseUrl => "https://www.saucedemo.com/cart.html";

        protected IWebElement ShoppingCartPageTitle => driver.FindElement(By.ClassName("title"));

        protected IWebElement RemoveButton => driver.FindElement(By.XPath("(//button[contains(@class,'button')])[1]"));

        protected IList<IWebElement> CartItems => driver.FindElements(By.ClassName("cart_item"));

        protected IWebElement ContinueShoppingButton => driver.FindElement(By.Id("continue-shopping"));

        protected IWebElement CheckoutButton => driver.FindElement(By.Id("checkout"));
    }
}
