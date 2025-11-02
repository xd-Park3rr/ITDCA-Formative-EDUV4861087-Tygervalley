using System;

namespace AVLTree
{
    // q2_ AVL node for Question 2
    public class q2_AVLNode
    {
        public q2_Book Data { get; set; }
        public q2_AVLNode? Left { get; set; }
        public q2_AVLNode? Right { get; set; }
        public int Height { get; set; }

        public q2_AVLNode(q2_Book book)
        {
            Data = book;
            Left = null;
            Right = null;
            Height = 1;
        }
    }
}
