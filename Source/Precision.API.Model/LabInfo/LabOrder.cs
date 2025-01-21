namespace Precision.API.Model.LabInfo
{
    public class LabOrder
    {
        public LabOrder()
        {
        }
        public string PatientChart { get; set; }
        public string PatientFirstName { get; set; }
        public string PatientMiddleInitial { get; set; }
        public string PatientLastName { get; set; }
        public string PatientGender { get; set; }
        public string PatientDOB { get; set; }
        public string CareOf { get; set; }
        public string PatientAddress1 { get; set; }
        public string PatientAddress2 { get; set; }
        public string PatientCity { get; set; }
        public string PatientState { get; set; }
        public string PatientZip { get; set; }
        public string PatientEmail { get; set; }
        public string PatientPhone { get; set; }
        public string PatientRace { get; set; }
        public string TestCode { get; set; }
        public string DiagnosisCode { get; set; }
        public string CollectionDate { get; set; }
        public string CollectionTime { get; set; }
        public string Source { get; set; }
        public string OrderComment { get; set; }
        public string PatientDocChart { get; set; }
        public string OrderNumber { get; set; }
    }
}