using SwagLabs_ShoppingCart.Models;

namespace SwagLabs_ShoppingCart.Pages
{
    public class CheckoutInformationPage : CheckoutInformationPageElements
    {
        public CheckoutInformationPage(IWebDriver driver) : base(driver)
        {
        }

        public string CheckPageTitle()
        {
            return CheckoutInformationPageTitle.Text;
        }

        public void FieldOutUserInformation(string firstName, string lastName, string zipCode)
        {
            FirstNameField.SendKeys(firstName);
            LastNameField.SendKeys(lastName);
            ZipCodeField.SendKeys(zipCode);        
        }

        public bool CheckoutInformationErrorMessageExist()
        {
            return CheckoutInformationErrorMessageLabel.Displayed;
        }

        public string GetCheckoutInformationErrorText()
        {
            return CheckoutInformationErrorMessageLabel.Text;
        }

        public void ContinueCheckout()
        {
            ContinueButton.Click();
        }

        public void CancelCheckout()
        {
            CancelButton.Click();
        }
    }
}
