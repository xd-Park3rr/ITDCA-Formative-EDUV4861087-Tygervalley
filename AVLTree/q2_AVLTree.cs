
using System.Reflection;

namespace AVLTree
{
    // wrapper around q1 tree to add extra features without changing q1 code
    public class q2_AVLTree
    {
        private readonly q1_AVLTree _baseTree = new q1_AVLTree();
        private readonly FieldInfo _rootField;

        public int Count { get; private set; }

        public q2_AVLTree()
        {
            Count = 0;
            // use reflection to access q1's private Root field
            _rootField = typeof(q1_AVLTree).GetField("Root", BindingFlags.Instance | BindingFlags.NonPublic)
                ?? throw new InvalidOperationException("Cannot access q1_AVLTree.Root via reflection");
        }

        public void Insert(q2_Book book)
        {
            var qb = new q1_Book(book.Title, book.Author, book.Year);
            _baseTree.Insert(qb);
            Count++;
        }

        public void Delete(int year, string title)
        {
            if (FindNodeInQ1(year, title) != null)
            {
                _baseTree.Delete(year, title);
                Count = Math.Max(0, Count - 1);
            }
        }

        // just return titles in order
        public IEnumerable<string> InOrderTitles()
        {
            foreach (var b in _baseTree.InOrder())
                yield return b.Title;
        }

        // search for a year using binary search
        public bool SearchByYear(int year)
        {
            var root = (q1_AVLNode?)_rootField.GetValue(_baseTree);
            var node = root;
            while (node != null)
            {
                if (year == node.Data.Year) return true;
                node = year < node.Data.Year ? node.Left : node.Right;
            }
            return false;
        }

        // get all books for a specific year
        public List<q2_Book> GetBooksByYear(int year)
        {
            var result = new List<q2_Book>();
            foreach (var b in _baseTree.InOrder())
            {
                if (b.Year == year)
                {
                    result.Add(new q2_Book(b.Title, b.Author, b.Year));
                }
            }
            return result;
        }

        // get rightmost node (most recent)
        public q2_Book? GetMostRecentBook()
        {
            var root = (q1_AVLNode?)_rootField.GetValue(_baseTree);
            if (root == null) return null;
            
            var node = root;
            while (node.Right != null) node = node.Right;
            
            var qb = node.Data;
            return new q2_Book(qb.Title, qb.Author, qb.Year);
        }

        private q1_AVLNode? FindNodeInQ1(int year, string title)
        {
            var node = (q1_AVLNode?)_rootField.GetValue(_baseTree);
            while (node != null)
            {
                if (year < node.Data.Year) node = node.Left;
                else if (year > node.Data.Year) node = node.Right;
                else
                {
                    int cmp = string.Compare(title, node.Data.Title, StringComparison.Ordinal);
                    if (cmp == 0) return node;
                    node = cmp < 0 ? node.Left : node.Right;
                }
            }
            return null;
        }
    }
 
}
