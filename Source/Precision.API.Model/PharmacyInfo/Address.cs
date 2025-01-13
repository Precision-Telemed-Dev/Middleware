namespace Precision.API.Model.PharmacyInfo
{
    public class Address
    {
        public Address()
        {
        }
        public string address1 { get; set; }
        public string? address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zipCode { get; set; }
    }
}