// q3_PriorityQueue: Priority queue implementation for clinic patient management
public class q3_PriorityQueue
{
    // Priority range constants
    private const int MinPriority = 0; // Most urgent
    private const int MaxPriority = 4; // Least urgent

    // Array of queues, one for each priority level
    // Index 0 = priority 0 (most urgent)
    // Index 4 = priority 4 (least urgent)
    private readonly Queue<string>[] queues;

    // Constructor - initializes a queue for each priority level (0-4)
    public q3_PriorityQueue()
    {
        // Create array with 5 queues (priorities 0-4)
        queues = new Queue<string>[MaxPriority - MinPriority + 1];
        // Initialize each queue
        for (int i = 0; i < queues.Length; i++) queues[i] = new Queue<string>();
    }
    
    // Enqueue a patient with their name and priority level
    public void Enqueue(string name, int priority)
    {
        // Validate patient name is not null
        if (name == null) throw new ArgumentNullException(nameof(name));
        
        if (priority < MinPriority || priority > MaxPriority)
            throw new ArgumentOutOfRangeException(nameof(priority));

        // Add patient to the queue corresponding to their priority level
        queues[priority].Enqueue(name);
    }
    
    // Dequeue the next patient with highest priority (lowest priority number)
    public string? Dequeue()
    {
        // Check queues in order from highest priority (0) to lowest (4)
        for (int p = MinPriority; p <= MaxPriority; p++)
        {
            // If this priority queue has patients, dequeue the first one
            if (queues[p].Count > 0) return queues[p].Dequeue();
        }

        // No patients in any queue
        return null;
    }
    
    // Check if all priority queues are empty
    public bool IsEmpty()
    {
        // Check each priority level
        for (int p = MinPriority; p <= MaxPriority; p++) if (queues[p].Count > 0) return false;
        return true;
    }
    
    // Returns the queue contents in priority order as formatted strings
    // Format: "[priority] name" where lower priority number = higher urgency
    public IEnumerable<string> ViewQueue()
    {
        // Iterate through priorities from most urgent (0) to least urgent (4)
        for (int p = MinPriority; p <= MaxPriority; p++)
        {
            // Return all patients at this priority level
            foreach (var name in queues[p])
            {
                yield return $"[{p}] {name}";
            }
        }
    }
}
