namespace checkoutservice.Models
{
    public class Price
    {
        public string CurrencyCode { get; set; }
        public int Units { get; set; }
        public double Nano { get; set; }
    }
}