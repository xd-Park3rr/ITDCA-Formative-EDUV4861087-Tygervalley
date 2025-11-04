
var pq = new q3_PriorityQueue();

Console.WriteLine("Priority queue interactive mode");
Console.WriteLine("Commands: ENQUEUE <name> <priority> | DEQUEUE | VIEW | EXIT");
Console.WriteLine("(Priority 0 = most urgent, 4 = least urgent)");

while (true)
{
	Console.Write("> ");
	string? line = Console.ReadLine();
	if (line == null) break;

	line = line.Trim();
	if (line.Length == 0) continue;
	if (line.Equals("EXIT", StringComparison.OrdinalIgnoreCase)) break;

	var parts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
	if (parts.Length == 0) continue;

	var cmd = parts[0].ToUpperInvariant();
	
	if (cmd == "ENQUEUE")
	{
		if (parts.Length < 3)
		{
			continue;
		}

		var name = parts[1];
		if (!int.TryParse(parts[2], out int priority)) continue;

		try
		{
			pq.Enqueue(name, priority);
		}
		catch (ArgumentOutOfRangeException)
		{
			continue;
		}
	}
	else if (cmd == "DEQUEUE")
	{
		var patient = pq.Dequeue();
		if (patient != null)
		{
			Console.WriteLine(patient);
		}
	}
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
		continue;
	}
}
