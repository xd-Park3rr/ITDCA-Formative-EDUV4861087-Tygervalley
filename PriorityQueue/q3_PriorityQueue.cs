public class q3_PriorityQueue
{
    private const int MinPriority = 0;
    private const int MaxPriority = 4;

    private readonly Queue<string>[] queues;

    public q3_PriorityQueue()
    {
        queues = new Queue<string>[MaxPriority - MinPriority + 1];
        for (int i = 0; i < queues.Length; i++) queues[i] = new Queue<string>();
    }
    public void Enqueue(string name, int priority)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (priority < MinPriority || priority > MaxPriority)
            throw new ArgumentOutOfRangeException(nameof(priority));

        queues[priority].Enqueue(name);
    }
    public string? Dequeue()
    {
        for (int p = MinPriority; p <= MaxPriority; p++)
        {
            if (queues[p].Count > 0) return queues[p].Dequeue();
        }

        return null;
    }
    public bool IsEmpty()
    {
        for (int p = MinPriority; p <= MaxPriority; p++) if (queues[p].Count > 0) return false;
        return true;
    }
    /// Returns the queue contents in priority order as formatted strings:
    /// "[priority] name" where lower priority number indicates higher urgency.
    public IEnumerable<string> ViewQueue()
    {
        for (int p = MinPriority; p <= MaxPriority; p++)
        {
            foreach (var name in queues[p])
            {
                yield return $"[{p}] {name}";
            }
        }
    }
}
