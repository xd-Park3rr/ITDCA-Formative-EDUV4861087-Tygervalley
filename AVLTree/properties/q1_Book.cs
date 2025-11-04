namespace AVLTree
{
    // q1_ Book class representing a book listing
    // Used as the data stored in AVL tree nodes
    public class q1_Book
    {
        public string Title { get; set; } // Book's title
        public string Author { get; set; } // Book's author name
        public int Year { get; set; } // Publication year (primary key for AVL tree ordering)
        public q1_Book(string title, string author, int year) // Constructor - creates a new book with the given properties
        {
            Title = title;
            Author = author;
            Year = year;
        }
        public override string ToString() // String representation of the book for display purposes
        {
            return $"{Year} - {Title} by {Author}";
        }
    }
}
