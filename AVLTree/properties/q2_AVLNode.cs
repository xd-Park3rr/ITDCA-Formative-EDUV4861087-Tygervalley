namespace AVLTree
{
    // q2_ AVL node for Question 2
    // (q2_AVLTree reuses q1_AVLNode through the q1_AVLTree wrapper)
    public class q2_AVLNode
    {
        
        public q2_Book Data { get; set; } // Book data stored in this node
        public q2_AVLNode? Left { get; set; } // Pointer to left child
        public q2_AVLNode? Right { get; set; } // Pointer to right child
        public int Height { get; set; } // Height of node for AVL balancing
        public q2_AVLNode(q2_Book book) // Constructor - creates a new leaf node
        {
            Data = book;
            Left = null;
            Right = null;
            Height = 1;
        }
    }
}
