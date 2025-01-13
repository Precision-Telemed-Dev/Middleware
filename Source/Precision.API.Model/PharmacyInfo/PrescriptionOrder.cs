namespace Precision.API.Model.PharmacyInfo
{
    public class PrescriptionOrder
    {
        public PrescriptionOrder()
        {
            prescription = new Prescription();
            prescriptionTransferDetails = new PrescriptionTransferDetails();
            prescriber = new Prescriber();
            patient = new Patient();
        }
        public string? requestType { get; set; }
        public string referenceId { get; set; }
        public Prescription prescription { get; set; }
        public PrescriptionTransferDetails prescriptionTransferDetails { get; set; }
        public Prescriber prescriber { get; set; }
        public Patient patient { get; set; }
    }
}