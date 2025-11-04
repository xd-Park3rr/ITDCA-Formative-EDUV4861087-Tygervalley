
namespace AVLTree
{
    // q1_ AVL tree node - represents a single node in the AVL tree
    public class q1_AVLNode
    {
        
        public q1_Book Data { get; set; } // Book data stored in this node
        public q1_AVLNode? Left { get; set; } // Pointer to left child (contains books with smaller year/title)
        public q1_AVLNode? Right { get; set; } // Pointer to right child (contains books with larger year/title)
        public int Height { get; set; } // Height of this node (used for balancing the tree)
        public q1_AVLNode(q1_Book book) // Constructor - creates a new leaf node with the given book
        {
            Data = book;
            Left = null;
            Right = null;
            Height = 1; // Leaf node height is 1
        }
    }
}
