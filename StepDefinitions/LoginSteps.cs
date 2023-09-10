using SwagLabs_ShoppingCart.Models;
using SwagLabs_ShoppingCart.Pages;
using TechTalk.SpecFlow;

namespace SwagLabs_ShoppingCart.Bindings
{
    [Binding]
    public class LoginSteps
    {
        private IWebDriver driver;
        private LoginPageElements loginPage;

        [BeforeScenario]
        public void BeforeScenario()
        {
            driver = new ChromeDriver();
            loginPage = new LoginPageElements(driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }

        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            loginPage.Open();
        }

        [When(@"I enter username ""(.*)"" and password ""(.*)""")]
        public void WhenIEnterValidCredentials(string username, string password)
        {
            loginPage.UsernameField.SendKeys(username);
            loginPage.PasswordField.SendKeys(password);
            loginPage.LoginButton.Click();
        }

        [Then(@"I should be taken to Products page")]
        public void ThenIShouldBeTakenToProductsPage()
        {
            Assert.That(new ProductsListPage(driver).IsPageOpen(), Is.True,
                Constants.PageNotFound);
        }
    }
}
