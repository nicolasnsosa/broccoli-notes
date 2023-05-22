using ReactiveUI;

namespace Broccoli.Messages;

public class OpenCrudWindowMessage<T>
{
    public T Data { get; set; }
}