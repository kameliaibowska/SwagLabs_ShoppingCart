﻿using SwagLabs_ShoppingCart.Models;
using SwagLabs_ShoppingCart.Models.DTO;

namespace SwagLabs_ShoppingCart.Pages
{
    public class ShoppingCartPage : ShoppingCartPageElements
    {
        private GenericPage genericPage;

        public ShoppingCartPage(IWebDriver driver) : base(driver)
        {
            genericPage = new GenericPage(driver);
        }

        public string CheckPageTitle()
        {
            return ShoppingCartPageTitle.Text;
        }

        public Product GetShoppingCartProductElements(IWebElement element)
        {
            return genericPage.GetProductElements(element);
        }

        public IList<Product> GetShoppingCartProducts()
        {
            var products = new List<Product>();

            foreach (IWebElement item in CartItems)
            {
                products.Add(GetShoppingCartProductElements(item));
            }
            return products;
        }

        public void RemoveProduct()
        {
            RemoveButton.Click();
        }

        public int GetCartItemsCount()
        {
            var cartItems = CartItems.Count();
            return cartItems;
        }

        public void ContinueShopping()
        {
            ContinueShoppingButton.Click();
        }

        public void Checkout()
        {
            CheckoutButton.Click();
        }
    }
}