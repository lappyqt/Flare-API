namespace Flare.Application.Models;

public class RequestErrorModel
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = String.Empty;
}