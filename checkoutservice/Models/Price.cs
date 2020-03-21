namespace checkoutservice.Models
{
    public class Price
    {
        public string currencyCode { get; set; }
        public int units { get; set; }
        public double nanos { get; set; }
    }
}