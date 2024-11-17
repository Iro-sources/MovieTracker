namespace MovieTracker.Models.V2
{
    public class Error
    {
        public bool Status { get; set; } = false;
        public string ErrorMessage { get; set; }
        public string? ErrorDetails { get; set; }




    }
}
