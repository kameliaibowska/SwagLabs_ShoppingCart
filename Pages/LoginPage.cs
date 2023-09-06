using SwagLabs_ShoppingCart.Models;

namespace SwagLabs_ShoppingCart.Pages
{
    public class LoginPage : LoginPageElements
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {
        }

        public string GetPageHeadingText()
        {
            return PageHeadingLabel.Text;
        }

        public bool PageErrorMessageExist()
        {
            return ErrorMessageLabel.Displayed;
        }

        public string GetPageErrorText()
        {
            return ErrorMessageLabel.Text;
        }

        public void Login(string username, string password)
        {
            UsernameField.SendKeys(username);
            PasswordField.SendKeys(password);
            LoginButton.Click();
        }  
    }
}
