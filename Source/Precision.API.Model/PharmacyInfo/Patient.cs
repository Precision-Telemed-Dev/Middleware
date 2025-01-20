namespace Precision.API.Model.PharmacyInfo
{
    public class Patient
    {
        public Patient()
        {
            address = new Address();
            allergies = new List<Allergy>();    
            conditions = new List<Condition>();
        }
        public string firstName { get; set; }
        public string? middleName { get; set; }
        public string lastName { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string? phoneNumber { get; set; }
        public string? email { get; set; }
        public Address address { get; set; }
        public List<Allergy> allergies { get; set; }
        public List<Condition> conditions { get; set; }
    }
}