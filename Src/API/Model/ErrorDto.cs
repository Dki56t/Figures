namespace API.Model
{
    public sealed class ErrorDto
    {
        public ErrorDto(string title, string detail)
        {
            Title = title;
            Detail = detail;
        }

        public string Title { get; }
        public string Detail { get; }
    }
}