using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Models
{
    public class LoginPageElements : BasePage
    {
        public LoginPageElements(IWebDriver driver) : base(driver)
        {
        }

        protected override string BaseUrl => "https://www.saucedemo.com/";

        public IWebElement UsernameField => driver.FindElement(By.Id("user-name"));

        public IWebElement PasswordField => driver.FindElement(By.Id("password"));

        public IWebElement LoginButton => driver.FindElement(By.Id("login-button"));

        protected IWebElement PageHeadingLabel => driver.FindElement(By.ClassName("login_logo"));

        protected IWebElement ErrorMessageLabel => driver.FindElement(By.ClassName("error-message-container"));
    }
}
