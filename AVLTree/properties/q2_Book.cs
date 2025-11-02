using System;

namespace AVLTree
{
    // q2_ Book class for Question 2
    public class q2_Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }

        public q2_Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public override string ToString() => $"{Year} - {Title} by {Author}";
    }
}
