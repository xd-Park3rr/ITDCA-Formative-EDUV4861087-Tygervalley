using System;
using System.Collections.Generic;
using System.Reflection;

namespace AVLTree
{
    // q2_ wrapper that reuses q1_AVLTree (keeps q1 code untouched)
    public class q2_AVLTree
    {
        private readonly q1_AVLTree _baseTree = new q1_AVLTree();
        private readonly FieldInfo _rootField;

        // Count maintained locally (updated on inserts/deletes)
        public int Count { get; private set; }

        public q2_AVLTree()
        {
            Count = 0;
            // access private field 'Root' from q1_AVLTree using reflection
            _rootField = typeof(q1_AVLTree).GetField("Root", BindingFlags.Instance | BindingFlags.NonPublic)
                ?? throw new InvalidOperationException("Cannot access q1_AVLTree.Root via reflection");
        }

        // Insert by delegating to q1_AVLTree and incrementing count
        public void Insert(q2_Book book)
        {
            // create q1_Book from q2_Book and insert into base tree
            var qb = new q1_Book(book.Title, book.Author, book.Year);
            _baseTree.Insert(qb);
            Count++;
        }

        // Delete: check if present (binary search using q1 structure) then delegate and decrement count
        public void Delete(int year, string title)
        {
            if (FindNodeInQ1(year, title) != null)
            {
                _baseTree.Delete(year, title);
                Count = Math.Max(0, Count - 1);
            }
        }

        // In-order traversal of titles: reuse q1 InOrder and map to titles
        public IEnumerable<string> InOrderTitles()
        {
            foreach (var b in _baseTree.InOrder())
                yield return b.Title;
        }

        // Binary search by year using actual q1 tree structure (O(h))
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

        // Most recent book: right-most node in q1 tree
        public q2_Book? GetMostRecentBook()
        {
            var root = (q1_AVLNode?)_rootField.GetValue(_baseTree);
            if (root == null) return null;
            var node = root;
            while (node.Right != null) node = node.Right;
            var qb = node.Data;
            return new q2_Book(qb.Title, qb.Author, qb.Year);
        }

        // Helper: find a node by both year and title in the q1 tree; returns the q1 node or null
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
