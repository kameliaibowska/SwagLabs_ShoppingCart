using SwagLabs_ShoppingCart.Models;
using SwagLabs_ShoppingCart.Pages;
using TechTalk.SpecFlow;

namespace SwagLabs_ShoppingCart.StepDefinitions
{
    public class BaseSteps
    {
        protected IWebDriver driver;

        public BaseSteps()
        {
            driver = new ChromeDriver();
        }

        protected void LoginWithValidCredentials()
        {
            var loginPage = new LoginPageElements(driver);
            loginPage.Open();

            loginPage.UsernameField.SendKeys(Constants.ValidUsername);
            loginPage.PasswordField.SendKeys(Constants.ValidPassword);
            loginPage.LoginButton.Click();
        }
    }
}
