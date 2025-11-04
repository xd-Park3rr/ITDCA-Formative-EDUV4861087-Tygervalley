
namespace AVLTree
{
	class Program
	{
		static void Main(string[] args)
		{
			var books = new List<q1_Book>
			{
				new q1_Book("The Silent Patient","Alex Michaelides",2019),
				new q1_Book("The Great Gatsby","F. Scott Fitzgerald",1925),
				new q1_Book("To Kill a Mockingbird","Harper Lee",1960),
				new q1_Book("1984","George Orwell",1949),
				new q1_Book("The Catcher in the Rye","J.D. Salinger",1951),
				new q1_Book("Pride and Prejudice","Jane Austen",1813),
				new q1_Book("Moby-Dick","Herman Melville",1851),
				new q1_Book("The Hobbit","J.R.R. Tolkien",1937),
				new q1_Book("Brave New World","Aldous Huxley",1932),
				new q1_Book("The Book Thief","Markus Zusak",2005),
				new q1_Book("The Road","Cormac McCarthy",2006),
				new q1_Book("Harry Potter and the Sorcerer's Stone","J.K. Rowling",1997),
				new q1_Book("The Girl with the Dragon Tattoo","Stieg Larsson",2005),
				new q1_Book("The Alchemist","Paulo Coelho",1988),
				new q1_Book("The Shining","Stephen King",1977),
				new q1_Book("Wuthering Heights","Emily Brontë",1847),
				new q1_Book("Catch-22","Joseph Heller",1961),
				new q1_Book("The Hunger Games","Suzanne Collins",2008),
				new q1_Book("The Da Vinci Code","Dan Brown",2003),
				new q1_Book("The Outsiders","S.E. Hinton",1967)
			};

			var tree = new q1_AVLTree();

			Console.WriteLine("Inserting books into AVL tree...");
			foreach (var b in books)
				tree.Insert(b);

			Console.WriteLine();
			Console.WriteLine("In-order traversal (sorted by year, then title):");
			PrintInOrder(tree);

			// Demonstrate deletion
			Console.WriteLine();
			Console.WriteLine("Deleting 'The Book Thief' (2005) and 'The Hobbit' (1937)...");
			tree.Delete(2005, "The Book Thief");
			tree.Delete(1937, "The Hobbit");

			Console.WriteLine();
			Console.WriteLine("In-order traversal after deletions:");
			PrintInOrder(tree);

			// --- Demonstrate Question 2 features using q2_ classes ---
			// Note: Q2 is a separate implementation demonstrating enhanced features.
			// It creates a new tree with fresh data to showcase the 4 required Q2 features:
			// 1. In-order traversal (titles only)
			// 2. Binary search by year
			// 3. Display most recent book
			// 4. Display total book count
			Console.WriteLine();
			Console.WriteLine("--- Question 2: q2_AVLTree features demo ---");

			var tree2 = new q2_AVLTree();
			// Insert same sample data as q2_Book instances (fresh tree for Q2 demo)
			foreach (var b in books)
			{
				tree2.Insert(new q2_Book(b.Title, b.Author, b.Year));
			}

			Console.WriteLine();
			Console.WriteLine("Q2 In-order traversal (titles only):");
			foreach (var title in tree2.InOrderTitles())
				Console.WriteLine(title);

			Console.WriteLine();
			int searchYear = 2005;
			Console.WriteLine($"Search by year {searchYear}: " + (tree2.SearchByYear(searchYear) ? "Found" : "Not found"));

			var recent = tree2.GetMostRecentBook();
			Console.WriteLine();
			Console.WriteLine("Most recent book:");
			if (recent != null) Console.WriteLine(recent.Title + " (" + recent.Year + ")");
			else Console.WriteLine("No books in tree");

			Console.WriteLine();
			Console.WriteLine("Total number of books in q2 tree: " + tree2.Count);
		}

		static void PrintInOrder(q1_AVLTree tree)
		{
			foreach (var b in tree.InOrder())
			{
				Console.WriteLine(b.ToString());
			}
		}
	}
}
