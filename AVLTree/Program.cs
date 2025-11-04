
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

			// --- Question 2: Enhanced features with user interaction ---
			// Q1 operations affect Q2 - convert Q1 tree contents to Q2
			Console.WriteLine();
			Console.WriteLine("--- Question 2: q2_AVLTree features ---");

			var tree2 = new q2_AVLTree();
			// Transfer all books from Q1 tree to Q2 tree (so Q1 deletions are reflected)
			foreach (var b in tree.InOrder())
			{
				tree2.Insert(new q2_Book(b.Title, b.Author, b.Year));
			}

			Console.WriteLine();
			Console.WriteLine("Books transferred from Q1 to Q2 tree.");
			Console.WriteLine($"Total number of books: {tree2.Count}");

			// Interactive menu for Q2 features
			bool running = true;
			while (running)
			{
				Console.WriteLine();
				Console.WriteLine("=== Q2 Menu ===");
				Console.WriteLine("1 - Search by year");
				Console.WriteLine("2 - Display most recent book");
				Console.WriteLine("3 - Display number of books");
				Console.WriteLine("4 - Display all titles");
				Console.WriteLine("5 - Exit");
				Console.Write("Choose an option: ");

				string? input = Console.ReadLine();
				
				switch (input)
				{
					case "1":
						Console.Write("Enter year to search: ");
						if (int.TryParse(Console.ReadLine(), out int year))
						{
							var booksInYear = tree2.GetBooksByYear(year);
							if (booksInYear.Count > 0)
							{
								Console.WriteLine($"\nFound {booksInYear.Count} book(s) from {year}:");
								foreach (var book in booksInYear)
								{
									Console.WriteLine($"  - {book.Title} by {book.Author}");
								}
							}
							else
							{
								Console.WriteLine($"No books found from year {year}");
							}
						}
						else
						{
							Console.WriteLine("Invalid year");
						}
						break;

					case "2":
						var recent = tree2.GetMostRecentBook();
						if (recent != null)
						{
							Console.WriteLine($"Most recent book: {recent.Title} by {recent.Author} ({recent.Year})");
						}
						else
						{
							Console.WriteLine("No books in tree");
						}
						break;

					case "3":
						Console.WriteLine($"Total books: {tree2.Count}");
						break;

					case "4":
						Console.WriteLine("\nAll book titles (in order):");
						foreach (var title in tree2.InOrderTitles())
						{
							Console.WriteLine("  " + title);
						}
						break;

					case "5":
						running = false;
						Console.WriteLine("Exiting...");
						break;

					default:
						Console.WriteLine("Invalid option");
						break;
				}
			}
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
