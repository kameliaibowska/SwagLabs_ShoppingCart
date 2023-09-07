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

        //public void FieldOutUserInformation(string firstName, string lastName, string zipCode)
        //{
        //    FirstNameField.SendKeys(firstName);
        //    LastNameField.SendKeys(lastName);
        //    ZipCodeField.SendKeys(zipCode);        
        //}

        //public bool CheckoutInformationErrorMessageExist()
        //{
        //    return CheckoutInformationErrorMessageLabel.Displayed;
        //}

        public async Task FieldOutUserInformationAsync(string firstName, string lastName, string zipCode)
        {
            await Task.Run(() =>
            {
                FirstNameField.SendKeys(firstName);
                LastNameField.SendKeys(lastName);
                ZipCodeField.SendKeys(zipCode);
            });
        }

        public async Task<bool> CheckoutInformationErrorMessageExistAsync()
        {
            return await Task.FromResult(CheckoutInformationErrorMessageLabel.Displayed);
        }

        public string GetCheckoutInformationErrorText()
        {
            return CheckoutInformationErrorMessageLabel.Text;
        }

        public async Task ContinueCheckoutAsync()
        {
            await Task.Run(() =>
            {
                ContinueButton.Click();
            });
        }

        public async Task CancelCheckoutAsync()
        {
            await Task.Run(() =>
            {
                CancelButton.Click();
            });
        }
    }
}
