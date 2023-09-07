using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Models
{
    public class GenericPageElements : BasePage
    {
        public GenericPageElements(IWebDriver driver) : base(driver)
        {
        }

        protected IWebElement ProductDetailsName => driver.FindElement(By.ClassName("inventory_details_name"));

        protected IWebElement ProductDetailsDescription => driver.FindElement(By.ClassName("inventory_details_desc"));

        protected IWebElement ProductDetailsPrice => driver.FindElement(By.ClassName("inventory_details_price"));

        protected IWebElement ProductName(IWebElement element)
        {
            return element.FindElement(By.ClassName("inventory_item_name"));
        }

        protected IWebElement ProductDescription(IWebElement element)
        {
            return element.FindElement(By.ClassName("inventory_item_desc"));
        }

        protected IWebElement ProductPrice(IWebElement element)
        {
            return element.FindElement(By.ClassName("inventory_item_price"));
        }

        protected IWebElement MenuButton => driver.FindElement(By.Id("react-burger-menu-btn"));

        protected IWebElement LogoutLink => driver.FindElement(By.Id("logout_sidebar_link"));
    }
}
