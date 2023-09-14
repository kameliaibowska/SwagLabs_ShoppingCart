using SwagLabs_ShoppingCart.Models;
using SwagLabs_ShoppingCart.Models.DTO;

namespace SwagLabs_ShoppingCart.Pages
{
    public class CheckoutOverviewPage : CheckoutOverviewPageElements
    {
        private GenericPage genericPage;

        public CheckoutOverviewPage(IWebDriver driver) : base(driver)
        {
            genericPage = new GenericPage(driver);
        }

        public string CheckPageTitle()
        {
            return CheckoutOverviewPageTitle.Text;
        }

        public int GetCheckoutOverviewItemsCount()
        {
            return CheckoutOverviewItems.Count();
        }

        private Product GetCheckoutOverviewProductElements(IWebElement element)
        {

            return genericPage.GetProductElements(element);
        }

        public IList<Product> GetCheckoutOverviewProducts()
        {
            var products = new List<Product>();

            foreach (IWebElement item in CheckoutOverviewItems)
            {
                products.Add(GetCheckoutOverviewProductElements(item));
            }
            return products;
        }

        public bool IsPaymentInformationLabelDisplayed()
        {
            return PaymentInformationLabel.Displayed;
        }

        public async Task<bool> IsPaymentInformationLabelDisplayedAsync()
        {
            return await Task.FromResult(IsPaymentInformationLabelDisplayed());
        }

        public string GetPaymentInformation ()
        {
            return PaymentInformationValue.Text;
        }

        public bool IsShippingInformationLabelDisplayed()
        {
            return ShippingInformationLabel.Displayed;
        }

        public async Task<bool> IsShippingInformationLabelDisplayedAsync()
        {
            return await Task.FromResult(IsShippingInformationLabelDisplayed());
        }

        public string GetShippingInformation()
        {
            return ShippingInformationValue.Text;
        }

        public bool IsPriceTotalLabelDisplayed()
        {
            return PriceTotalLabel.Displayed;
        }

        public double GetItemTotalValue()
        {
            var itemTotal = ItemTotal.Text.Replace("Item total: $", "").Trim();
            return double.Parse(itemTotal);
        }

        public double GetTaxValue()
        {
            var tax = Tax.Text.Replace("Tax: $", "").Trim();
            return double.Parse(tax);
        }

        public double GetTotalValue()
        {
            var total = Total.Text.Replace("Total: $", "").Trim();
            return double.Parse(total);
        }

        public async Task FinishOrderAsync()
        {
            await Task.Run(() =>
            {
                FinishButton.Click();
            });
        }

        public void CancelOrder()
        {
            CancelButton.Click();
        }
    }
}
