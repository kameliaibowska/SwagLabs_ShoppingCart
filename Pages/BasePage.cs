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

        protected virtual string Url { get; }

        public void Open()
        {
            driver.Navigate().GoToUrl(GetFullUrl());
        }

        public bool IsPageOpen()
        {
            return driver.Url == GetFullUrl();
        }

        public string GetPageTitle()
        {
            return driver.Title;
        }

        private string GetFullUrl()
        {
            return $"{Constants.BaseUrl}{Url}";
        }
    }
}