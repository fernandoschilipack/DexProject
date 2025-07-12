namespace DexApi.Models
{
    public class DexLaneMeter
    {
        public DexLaneMeter(string productIdentifier, decimal price, int numberOfVends, decimal valueOfPaidSales)
        {
            ProductIdentifier = productIdentifier;
            Price = price;
            NumberOfVends = numberOfVends;
            ValueOfPaidSales = valueOfPaidSales;
        }

        public string ProductIdentifier { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int NumberOfVends { get; set; }
        public decimal ValueOfPaidSales { get; set; }
    }
}
