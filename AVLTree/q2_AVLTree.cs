
using System.Reflection;

namespace AVLTree
{
    // q2_ wrapper that reuses q1_AVLTree (keeps q1 code untouched)
    // Extends functionality with count tracking, search, and most recent book retrieval
    public class q2_AVLTree
    {
        // Instance of the q1 AVL tree for actual storage
        private readonly q1_AVLTree _baseTree = new q1_AVLTree();
        // Reflection field to access q1's private Root node
        private readonly FieldInfo _rootField;

        // Count maintained locally (updated on inserts/deletes)
        public int Count { get; private set; }

        // Constructor - initializes count and sets up reflection access to q1 root
        public q2_AVLTree()
        {
            Count = 0;
            // Access private field 'Root' from q1_AVLTree using reflection
            // This allows q2 to navigate the tree without modifying q1 code
            _rootField = typeof(q1_AVLTree).GetField("Root", BindingFlags.Instance | BindingFlags.NonPublic)
                ?? throw new InvalidOperationException("Cannot access q1_AVLTree.Root via reflection");
        }

        // Insert a q2_Book by converting it to q1_Book and delegating to base tree
        public void Insert(q2_Book book)
        {
            // Create q1_Book from q2_Book (converting between wrapper types)
            var qb = new q1_Book(book.Title, book.Author, book.Year);
            // Insert into the underlying q1 AVL tree
            _baseTree.Insert(qb);
            // Increment count after successful insertion
            Count++;
        }

        // Delete a book by year and title if it exists in the tree
        public void Delete(int year, string title)
        {
            // First check if the book exists before attempting deletion
            if (FindNodeInQ1(year, title) != null)
            {
                // Delegate deletion to base tree
                _baseTree.Delete(year, title);
                // Decrement count (ensure it doesn't go below 0)
                Count = Math.Max(0, Count - 1);
            }
        }

        // In-order traversal returning only book titles (sorted by year then title)
        public IEnumerable<string> InOrderTitles()
        {
            // Reuse q1's in-order traversal and extract just the titles
            foreach (var b in _baseTree.InOrder())
                yield return b.Title;
        }

        // Binary search to check if any book with the given year exists
        // Time complexity: O(h) where h is the height of the tree
        public bool SearchByYear(int year)
        {
            // Get the root node using reflection
            var root = (q1_AVLNode?)_rootField.GetValue(_baseTree);
            var node = root;
            // Traverse the tree using BST search
            while (node != null)
            {
                // Found a book with this year
                if (year == node.Data.Year) return true;
                // Navigate left or right based on comparison
                node = year < node.Data.Year ? node.Left : node.Right;
            }
            // Year not found in tree
            return false;
        }

        // Get the most recent book (rightmost node = largest year/latest title)
        public q2_Book? GetMostRecentBook()
        {
            // Get the root node using reflection
            var root = (q1_AVLNode?)_rootField.GetValue(_baseTree);
            // Empty tree - no books
            if (root == null) return null;
            
            var node = root;
            // Traverse to the rightmost node (largest values)
            while (node.Right != null) node = node.Right;
            
            // Convert q1_Book to q2_Book and return
            var qb = node.Data;
            return new q2_Book(qb.Title, qb.Author, qb.Year);
        }

        // Helper: find a node by both year and title in the q1 tree
        // Returns the q1_AVLNode if found, or null if not found
        private q1_AVLNode? FindNodeInQ1(int year, string title)
        {
            // Get root using reflection
            var node = (q1_AVLNode?)_rootField.GetValue(_baseTree);
            // Binary search through the tree
            while (node != null)
            {
                // Navigate based on year comparison
                if (year < node.Data.Year) node = node.Left;
                else if (year > node.Data.Year) node = node.Right;
                else
                {
                    // Same year - compare titles to find exact match
                    int cmp = string.Compare(title, node.Data.Title, StringComparison.Ordinal);
                    if (cmp == 0) return node; // Found exact match
                    // Navigate left or right based on title comparison
                    node = cmp < 0 ? node.Left : node.Right;
                }
            }
            // Not found
            return null;
        }
    }
 
}
