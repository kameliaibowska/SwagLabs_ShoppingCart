using SwagLabs_ShoppingCart.Models;

namespace SwagLabs_ShoppingCart.Pages
{
    public class ShoppingCartIconPage : ShoppingCartIconElements
    {
        public ShoppingCartIconPage(IWebDriver driver) : base(driver)
        {
        }

        public int GetShoppingCartItems()
        {
            var cartItemCount = int.Parse(ShoppingCartItemsCount.Text);

            return cartItemCount;
        }

        public int VerifyShoppingCartIsEmpty()
        {
            var cartContent = ShoppingCart.Text.Length;

            return cartContent;
        }

        public async Task GoToShoppingCartAsync()
        {
            await Task.Run(() =>
            {
                ShoppingCart.Click();
            });
        }
    }
}
