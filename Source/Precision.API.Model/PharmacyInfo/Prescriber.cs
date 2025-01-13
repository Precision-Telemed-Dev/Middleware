namespace Precision.API.Model.PharmacyInfo
{
    public class Prescriber
    {
        public Prescriber()
        {
            address = new Address();
        }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string npi { get; set; }
        public string phoneNumber { get; set; }
        public Address address { get; set; }
    }
}