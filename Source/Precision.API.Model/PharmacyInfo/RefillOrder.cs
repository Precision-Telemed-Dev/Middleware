namespace Precision.API.Model.PharmacyInfo
{
    public class RefillOrder
    {
        public RefillOrder()
        {
            patient = new Patient();
        }
        public string referenceId { get; set; }
        public string externalRxNumber { get; set; }
        public Patient patient { get; set; }
    }
}