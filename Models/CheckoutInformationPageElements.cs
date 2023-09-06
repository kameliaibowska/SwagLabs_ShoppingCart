using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Models
{
    public class CheckoutInformationPageElements : BasePage
    {
        public CheckoutInformationPageElements(IWebDriver driver) : base(driver)
        {
        }

        protected override string BaseUrl => "https://www.saucedemo.com/checkout-step-one.html";

        protected IWebElement CheckoutInformationPageTitle => driver.FindElement(By.ClassName("title"));

        protected IWebElement FirstNameField => driver.FindElement(By.Id("first-name"));

        protected IWebElement LastNameField => driver.FindElement(By.Id("last-name"));

        protected IWebElement ZipCodeField => driver.FindElement(By.Id("postal-code"));

        protected IWebElement ContinueButton => driver.FindElement(By.Id("continue"));

        protected IWebElement CancelButton => driver.FindElement(By.Id("cancel"));

        protected IWebElement CheckoutInformationErrorMessageLabel => 
            driver.FindElement(By.ClassName("error-message-container"));
    }
}
