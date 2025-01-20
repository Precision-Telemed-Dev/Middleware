namespace Precision.API.Model.Common
{
    public class Credential
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Mode { get; set; }
        public string? Url { get; set; }
        public string? SessionKey { get; set; }
    }
    public class PharmacyCredential
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Url { get; set; }
    }
}