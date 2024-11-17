namespace MovieTracker.Models.V2
{
    public class Result<T>
    {
        public Content<T> Content { get; set; }
        public Error Error { get; set; }

        public Result(Content<T> content)
        {
            Content = content;

        }

        public Result(Error error)
        {
            Error = error;
        }
    }
}
