namespace Precision.API.Model.PharmacyInfo
{
    public class Prescription
    {
        public Prescription()
        {
        }
        public float daysSupply { get; set; }
        public string directions { get; set; }
        public bool dispenseAsWritten { get; set; }
        public float dosage { get; set; }
        public string expirationDate { get; set; }
        public string ndc { get; set; }
        public string productDescription { get; set; }
        public float quantity { get; set; }
        public float remainingRefills { get; set; }
        public float totalRefills { get; set; }
        public string externalRxNumber { get; set; }
    }
}