
namespace AVLTree
{
    // q1_ AVL tree node
    public class q1_AVLNode
    {
        public q1_Book Data { get; set; }
        public q1_AVLNode? Left { get; set; }
        public q1_AVLNode? Right { get; set; }
        public int Height { get; set; }

        public q1_AVLNode(q1_Book book)
        {
            Data = book;
            Left = null;
            Right = null;
            Height = 1; // leaf node height = 1
        }
    }
}
