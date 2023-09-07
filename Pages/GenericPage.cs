using SwagLabs_ShoppingCart.Models;
using SwagLabs_ShoppingCart.Models.DTO;

namespace SwagLabs_ShoppingCart.Pages
{
    public class GenericPage : GenericPageElements
    {
        public GenericPage(IWebDriver driver) : base(driver)
        {
        }

        public void Logout()
        {
            MenuButton.Click();
            LogoutLink.Click();
        }
        
        public Product GetProductElements(IWebElement element)
        {
            var product = new Product();
            product.ProductTitle = ProductName(element).Text;
            product.ProductDescription = ProductDescription(element).Text;
            product.ProductPrice = Price(ProductPrice(element).Text);

            return product;
        }

        public Product GetProductDetailsElements()
        {
            var product = new Product();
            product.ProductTitle = ProductDetailsName.Text;
            product.ProductDescription = ProductDetailsDescription.Text;
            product.ProductPrice = Price(ProductDetailsPrice.Text);

            return product;
        }

        private double Price(string productPrice)
        {
            return double.Parse(productPrice.Replace("$", "").Trim());
        }
    }
}
