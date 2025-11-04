// q3_ Priority Queue Demo Program
// Interactive patient queue management system for clinic
// Priority: 0 = most urgent, 4 = least urgent

var pq = new q3_PriorityQueue(); // Create priority queue instance

Console.WriteLine("Priority queue interactive mode");
Console.WriteLine("Commands: ENQUEUE <name> <priority> | DEQUEUE | VIEW | EXIT");
Console.WriteLine("(Priority 0 = most urgent, 4 = least urgent)");

while (true)
{
	Console.Write("> ");
	string? line = Console.ReadLine();
	if (line == null) break; // EOF

	line = line.Trim();
	if (line.Length == 0) continue; // Skip empty lines
	if (line.Equals("EXIT", StringComparison.OrdinalIgnoreCase)) break;

	// Parse command and arguments
	var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
	if (parts.Length == 0) continue;

	var cmd = parts[0].ToUpperInvariant();
	
	// ENQUEUE command: Add patient to queue
	if (cmd == "ENQUEUE")
	{
		if (parts.Length < 3)
		{
			// Not enough arguments; skip this line
			continue;
		}

		// Extract patient name and priority
		var name = parts[1];
		if (!int.TryParse(parts[2], out int priority)) continue;

		try
		{
			// Add patient to priority queue
			pq.Enqueue(name, priority);
		}
		catch (ArgumentOutOfRangeException)
		{
			// Invalid priority (not 0-4); ignore
			continue;
		}
	}
	// DEQUEUE command: Remove and print next highest priority patient
	else if (cmd == "DEQUEUE")
	{
		var patient = pq.Dequeue();
		if (patient != null)
		{
			// Print the dequeued patient's name
			Console.WriteLine(patient);
		}
	}
	// VIEW command: Display all patients in priority order
	else if (cmd == "VIEW")
	{
		if (pq.IsEmpty())
		{
			Console.WriteLine("(empty)");
			continue;
		}

		foreach (var entry in pq.ViewQueue())
		{
			Console.WriteLine(entry);
		}
	}
	else
	{
		// Unknown command - ignore
		continue;
	}
}
