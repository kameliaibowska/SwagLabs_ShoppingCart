using SwagLabs_ShoppingCart.Models;
using SwagLabs_ShoppingCart.Models.DTO;

namespace SwagLabs_ShoppingCart.Pages
{
    public class ProductDetailsPage : ProductDetailsPageElements
    {
        private GenericPage genericPage;

        public ProductDetailsPage(IWebDriver driver, int i) : base(driver, i)
        {
            genericPage = new GenericPage(driver);
        }
        
        public void BackToProductsList()
        {
            BackToProductsButton.Click();
        }
        
        public void PressAddToCartRemoveButton()
        {
            AddToCartButton.Click();
        }

        public Product GetProductElements()
        {
            return genericPage.GetProductDetailsElements();
        }

        public string GetProductUrl()
        {
            return ItemUrl();
        }
    }
}
