namespace SwagLabs_ShoppingCart
{
    public interface Constants
    {
        // login productsPage
        public const string LoginPageHeader = "Swag Labs";

        public const string ValidUsername = "standard_user";

        public const string InvalidUsername = "standard_users";

        public const string LockedOutUsername = "locked_out_user";

        public const string ProblemUsername = "problem_user";

        public const string PerformanceGlitchUsername = "performance_glitch_user";

        public const string ValidPassword = "secret_sauce";

        public const string InvalidPassword = "secret_sauces";

        public const string LockedOutUserErrorMessage = "Epic sadface: Sorry, this user has been locked out.";

        public const string MissingCredentialsErrorMessage = "Epic sadface: Username is required";

        public const string InvalidCredentialsErrorMessage = "Epic sadface: Username and password do not match any user in this service";

        // Products list productsPage
        public const string ProductsPageHeader = "Products";

        public const string PriceHighToLow = "hilo";

        public const string PriceLowToHigh = "lohi";

        public const string NameAtoZ = "az";

        public const string NameZtoA = "za";

        // Shopping Cart productsPage
        public const string ShoppingCartTitle = "Your Cart";

        // Checkout Information productsPage
        public const string CheckoutInformationTitle = "Checkout: Your Information";

        public const string CheckoutInformationFirstName = "FName";

        public const string CheckoutInformationLastName = "LName";

        public const string CheckoutInformationZipCode = "ZipCode";

        public const string CheckoutInformationFirstNameErrorMessage = "Error: First Name is required";

        public const string CheckoutInformationLastNameErrorMessage = "Error: Last Name is required";

        public const string CheckoutInformationZipCodeErrorMessage = "Error: Postal Code is required";

        // Checkput overview productsPage
        public const string CheckoutOverviewTitle = "Checkout: Overview";

        public const string SauceCard = "SauceCard #31337";

        public const string SauceShoppingInformation = "Free Pony Express Delivery!";

        // Checkout complete productsPage
        public const string CheckoutCompleteTitle = "Checkout: Complete!";

        public const string CompleteMessageHeader = "Thank you for your order!";

        public const string CompleteMessageDescription = "Your order has been dispatched, and will arrive just as fast as the pony can get there!";

        // error messages
        public const string PageNotFound = "Page not found!";

        public const string IncorrectPageTitle = "Incorrect Page Title!";

        public const string IncorectErrorMessage = "Incorrect error message!";

        public const string MissingErrorMessage = "Missing error message!";

        public const string IncorrectUrl = "Incorrect url!";

        public const string IncorrectShoppingCartCount = "Shopping cart count is incorrect!";

        public const string ShoppingCartIsNotEmpty = "Shopping cart is not empty!";

        public const string ProductsAreNotSame = "Products are not the same!";

        public const string MissingProducts = "Products are missing!";
    }
}
