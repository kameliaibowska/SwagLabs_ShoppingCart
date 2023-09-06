namespace SwagLabs_ShoppingCart.Models.DTO
{
    public class Product
    {
        public string? ProductTitle { get; set; }

        public string? ProductDescription { get; set; }

        public double ProductPrice { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Product))
                return false;

            Product other = obj as Product;

            return ProductTitle == other.ProductTitle &&
                   ProductDescription == other.ProductDescription &&
                   ProductPrice == other.ProductPrice;
        }

        public override int GetHashCode()
        {
            // Use XOR (^) for combining hash codes.
            return (ProductTitle?.GetHashCode() ?? 0) ^
                   (ProductDescription?.GetHashCode() ?? 0) ^
                   ProductPrice.GetHashCode();
        }
    }
}
