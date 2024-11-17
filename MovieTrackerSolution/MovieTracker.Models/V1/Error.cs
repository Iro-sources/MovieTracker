namespace MovieTracker.Models.V1
{
    public class Error
    {
        public bool Status { get; set; }
        public string ErrorMessage { get; set; }
        public string? ErrorDetails { get; set; }
    }
}
