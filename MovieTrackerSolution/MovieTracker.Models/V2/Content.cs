namespace MovieTracker.Models.V2
{
    public class Content<T>
    {
        public bool Status { get; set; } = true;
        public T Data { get; set; }


        public Content(T data)
        {
            Data = data;
        }


    }
}
