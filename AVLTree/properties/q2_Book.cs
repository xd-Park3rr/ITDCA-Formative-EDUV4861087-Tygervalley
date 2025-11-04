namespace AVLTree
{
    // q2_ Book class for Question 2
    // Represents a book with title, author, and publication year
    public class q2_Book
    {
        public string Title { get; set; } // Book's title
        public string Author { get; set; } // Book's author name
        public int Year { get; set; } // Publication year
        public q2_Book(string title, string author, int year) // Constructor - creates a new book instance
        {
            Title = title;
            Author = author;
            Year = year;
        }
        public override string ToString() => $"{Year} - {Title} by {Author}"; // Compact string representation using expression-bodied member
    }
}
