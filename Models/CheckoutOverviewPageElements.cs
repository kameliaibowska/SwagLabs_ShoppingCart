using SwagLabs_ShoppingCart.Pages;

namespace SwagLabs_ShoppingCart.Models
{
    public class CheckoutOverviewPageElements : BasePage
    {
        public CheckoutOverviewPageElements(IWebDriver driver) : base(driver)
        {
        }

        protected override string BaseUrl => "https://www.saucedemo.com/checkout-step-two.html";

        protected IWebElement CheckoutOverviewPageTitle => driver.FindElement(By.ClassName("title"));

        protected IList<IWebElement> CheckoutOverviewItems => driver.FindElements(By.ClassName("cart_item"));

        protected IWebElement PaymentInformationLabel => driver.FindElement(By.XPath("(//div[@class='summary_info_label'])[1]"));

        protected IWebElement PaymentInformationValue => driver.FindElement(By.XPath("(//div[@class='summary_value_label'])[1]"));

        protected IWebElement ShippingInformationLabel => driver.FindElement(By.XPath("(//div[@class='summary_info_label'])[2]"));

        protected IWebElement ShippingInformationValue => driver.FindElement(By.XPath("(//div[@class='summary_value_label'])[2]"));

        protected IWebElement PriceTotalLabel => driver.FindElement(By.XPath("(//div[@class='summary_info_label'])[3]"));

        protected IWebElement ItemTotal => driver.FindElement(By.ClassName("summary_subtotal_label"));

        protected IWebElement Tax => driver.FindElement(By.ClassName("summary_tax_label"));

        protected IWebElement Total => driver.FindElement(By.ClassName("summary_total_label"));

        protected IWebElement CancelButton => driver.FindElement(By.Id("cancel"));

        protected IWebElement FinishButton => driver.FindElement(By.Id("finish"));
    }
}
