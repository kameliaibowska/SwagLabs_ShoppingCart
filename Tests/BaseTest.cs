namespace SwagLabs_ShoppingCart.Tests
{
    public class BaseTest
    {
        protected IWebDriver driver;


        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void CloseBrowser()
        {
            this.driver.Quit();
        }
    }
}
