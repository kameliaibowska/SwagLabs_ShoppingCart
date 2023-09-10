using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    [TestFixture(Constants.ValidUsername, Constants.ValidPassword)]
    [TestFixture(Constants.PerformanceGlitchUsername, Constants.ValidPassword)]
    // uncomment to check errors for the problem user
    // [TestFixture(Constants.ProblemUsername, Constants.ValidPassword)]
    public class BaseTest : RootTest
    {
        private readonly string username;
        private readonly string password;
        private LoginPage loginPage;
        private GenericPage genericPage;

        public BaseTest(string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        [SetUp]
        public void Setup()
        {
            loginPage = new LoginPage(driver);
            loginPage.Open();
            loginPage.Login(username, password);
            genericPage = new GenericPage(driver);
        }

        [TearDown]
        public void CloseBrowser()
        {
            genericPage.Logout();
            driver.Quit();
        }
    }
}
