namespace Application;

public class NotificationService
{
    private readonly Dictionary<Guid, Func<string, Task>> _subscribers = new();

    public void Subscribe(Guid userId, Func<string, Task> callback)
    {
        _subscribers[userId] = callback;
    }

    public void Unsubscribe(Guid userId)
    {
        _subscribers.Remove(userId);
    }

    public void Notify(Guid userId, string jobId)
    {
        if (_subscribers.TryGetValue(userId, out var callback))
        {
            callback.Invoke(jobId);
        }
    }
}