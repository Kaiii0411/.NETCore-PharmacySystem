namespace PharmacySystem.Models
{
    public class RequestResponse
    {
        public Code StatusCode { get; set; }
        public string Content { get; set; }
        public string Message { get; set; }
    }
    public enum Code
    {
        Success = 0,
        Failed = 1,
        NotFound = 2
    }
}
