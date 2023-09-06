namespace SwagLabs_ShoppingCart.Pages
{
    public class BasePage
    {
        protected readonly IWebDriver driver;

        public BasePage(IWebDriver driver)
        {
            this.driver = driver;
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        protected virtual string BaseUrl { get; }

        public void Open()
        {
            driver.Navigate().GoToUrl(BaseUrl);
        }

        public bool IsPageOpen()
        {
            return driver.Url == BaseUrl;
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }
    }
}