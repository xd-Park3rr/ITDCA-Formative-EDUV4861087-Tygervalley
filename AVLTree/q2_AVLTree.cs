
namespace AVLTree
{
    // Q2 wrapper - adds extra features to Q1
    public class q2_AVLTree
    {
        private readonly q1_AVLTree _baseTree = new q1_AVLTree();

        public int Count { get; private set; }

        public q2_AVLTree()
        {
            Count = 0;
        }

        public void Insert(q2_Book book)
        {
            var q1Book = new q1_Book(book.Title, book.Author, book.Year);
            _baseTree.Insert(q1Book);
            Count++;
        }

        public void Delete(int year, string title)
        {
            // check if it exists first
            bool exists = _baseTree.InOrder().Any(b => b.Year == year && b.Title == title);
            if (exists)
            {
                _baseTree.Delete(year, title);
                Count--;
            }
        }

        public IEnumerable<string> InOrderTitles()
        {
            foreach (var b in _baseTree.InOrder())
                yield return b.Title;
        }

        public bool SearchByYear(int year)
        {
            return _baseTree.InOrder().Any(b => b.Year == year);
        }

        public List<q2_Book> GetBooksByYear(int year)
        {
            var result = new List<q2_Book>();
            foreach (var b in _baseTree.InOrder())
            {
                if (b.Year == year)
                    result.Add(new q2_Book(b.Title, b.Author, b.Year));
            }
            return result;
        }

        public q2_Book? GetMostRecentBook()
        {
            var last = _baseTree.InOrder().LastOrDefault();
            return last != null ? new q2_Book(last.Title, last.Author, last.Year) : null;
        }
    }
 
}
