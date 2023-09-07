using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Tests
{
    public class LoginPageTests : RootTest, Constants
    {
        private LoginPage page;

        [SetUp]
        public void Setup()
        {
            page = new LoginPage(driver);
        }

        [TestCase(Constants.LockedOutUsername, Constants.ValidPassword, Constants.LockedOutUserErrorMessage)]
        [TestCase("", "", Constants.MissingCredentialsErrorMessage)]
        [TestCase(Constants.InvalidUsername, Constants.ValidPassword, Constants.InvalidCredentialsErrorMessage)]
        [TestCase(Constants.ValidUsername, Constants.InvalidPassword, Constants.InvalidCredentialsErrorMessage)]
        [TestCase(Constants.InvalidUsername, Constants.InvalidPassword, Constants.InvalidCredentialsErrorMessage)]
        public void LoginWithInvalidCredentials(string username, string password, string errorMessage)
        {
            page.Open();

            Assert.That(page.GetPageHeadingText(), Is.EqualTo(Constants.LoginPageHeader));

            page.Login(username, password);

            Assert.That(page.PageErrorMessageExist(), Is.True);
            Assert.That(page.GetPageErrorText(), Is.EqualTo(errorMessage));
        }

        [TestCase(Constants.ValidUsername, Constants.ValidPassword)]
        [TestCase(Constants.ProblemUsername, Constants.ValidPassword)]
        [TestCase(Constants.PerformanceGlitchUsername, Constants.ValidPassword)]
        public void LoginWithValidCredentials(string username, string password)
        {
            page.Open();

            Assert.That(page.GetPageHeadingText(), Is.EqualTo(Constants.LoginPageHeader));

            page.Login(username, password);
            Assert.That(new ProductsListPage(driver).IsPageOpen(), Is.True);
        }
    }
}
