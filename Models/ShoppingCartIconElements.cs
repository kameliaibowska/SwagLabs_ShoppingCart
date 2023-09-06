using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Models
{
    public class ShoppingCartIconElements : BasePage
    {
        public ShoppingCartIconElements(IWebDriver driver) : base(driver)
        {
        }

        protected IWebElement ShoppingCart => driver.FindElement(By.ClassName("shopping_cart_link"));

        protected IWebElement ShoppingCartItemsCount => driver.FindElement(By.ClassName("shopping_cart_badge"));
    }
}
