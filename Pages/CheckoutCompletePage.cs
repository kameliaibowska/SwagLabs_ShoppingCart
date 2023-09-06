using SwagLabs_ShoppingCart.Models;

namespace SwagLabs_ShoppingCart.Pages
{
    public class CheckoutCompletePage : CheckoutCompletePageElements
    {
        public CheckoutCompletePage(IWebDriver driver) : base(driver)
        {
        }

        public string CheckCompletePageTitle()
        {
            return CheckoutCompletePageTitle.Text;
        }

        public bool CheckCompleteImage()
        {
            return CompleteImage.Displayed;
        }

        public string CheckCompleteMessege()
        {
            return CheckoutCompletePageHeader.Text;
        }

        public string CheckCompleteDescription()
        {
            return CheckoutCompletePageText.Text;
        }

        public void GoBackToHome()
        {
            BackToHomeButton.Click();
        }
    }
}
