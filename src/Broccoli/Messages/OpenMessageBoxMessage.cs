namespace Broccoli.Messages;

public class OpenMessageBoxMessage<TCaller>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string[] Options { get; set; }

    public object? Payload { get; set; }
}
