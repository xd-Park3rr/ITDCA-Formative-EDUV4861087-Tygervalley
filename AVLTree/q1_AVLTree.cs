namespace AVLTree
{
    // AVL tree for storing books, ordered by year then title
    public class q1_AVLTree
    {
        private q1_AVLNode? Root;

        public q1_AVLTree()
        {
            Root = null;
        }

        public void Insert(q1_Book book) 
        {
            Root = InsertNode(Root, book);
        }

        public void Delete(int year, string title)
        {
            Root = DeleteNode(Root, year, title);
        }

        public IEnumerable<q1_Book> InOrder()
        {
            return InOrderNodes(Root);
        }

        // in-order traversal - returns books sorted
        private IEnumerable<q1_Book> InOrderNodes(q1_AVLNode? node)
        {
            if (node == null)
                yield break;

            // left subtree first
            foreach (var b in InOrderNodes(node.Left))
                yield return b;

            yield return node.Data;

            // then right subtree
            foreach (var b in InOrderNodes(node.Right))
                yield return b;
        }
        
        private q1_AVLNode InsertNode(q1_AVLNode? node, q1_Book book)
        {
            if (node == null)
                return new q1_AVLNode(book);

            // standard BST insert - compare year first, then title
            if (book.Year < node.Data.Year)
                node.Left = InsertNode(node.Left, book);
            else if (book.Year > node.Data.Year)
                node.Right = InsertNode(node.Right, book);
            else
            {
                // same year so use title to decide
                int cmp = string.Compare(book.Title, node.Data.Title, StringComparison.Ordinal);
                if (cmp < 0)
                    node.Left = InsertNode(node.Left, book);
                else
                    node.Right = InsertNode(node.Right, book);
            }

            // update height after insert
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // check if tree is unbalanced
            int balance = GetBalance(node);

            // Left Left case
            if (balance > 1 && book.Year < node.Left!.Data.Year)
                return RightRotate(node);

            // Right Right case
            if (balance < -1 && book.Year > node.Right!.Data.Year)
                return LeftRotate(node);

            // Left Right case
            if (balance > 1 && book.Year > node.Left!.Data.Year)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }

            // Right Left case
            if (balance < -1 && book.Year < node.Right!.Data.Year)
            {
                node.Right = RightRotate(node.Right!);
                return LeftRotate(node);
            }

            return node;
        }

        private q1_AVLNode? DeleteNode(q1_AVLNode? node, int year, string title)
        {
            if (node == null)
                return null;

            // BST delete - find the node first
            if (year < node.Data.Year)
            {
                node.Left = DeleteNode(node.Left, year, title);
            }
            else if (year > node.Data.Year)
            {
                node.Right = DeleteNode(node.Right, year, title);
            }
            else
            {
                // matching year, check title
                int cmp = string.Compare(title, node.Data.Title, StringComparison.Ordinal);
                if (cmp < 0)
                {
                    node.Left = DeleteNode(node.Left, year, title);
                }
                else if (cmp > 0)
                {
                    node.Right = DeleteNode(node.Right, year, title);
                }
                else
                {
                    // found it - this is the node to delete
                    if (node.Left == null || node.Right == null)
                    {
                        // node with one or no child
                        q1_AVLNode? temp = node.Left ?? node.Right;

                        if (temp == null)
                        {
                            node = null;
                        }
                        else
                        {
                            node = temp;
                        }
                    }
                    else
                    {
                        // two children - get successor (smallest in right subtree)
                        q1_AVLNode temp = MinValueNode(node.Right!);
                        node.Data = temp.Data;
                        node.Right = DeleteNode(node.Right, temp.Data.Year, temp.Data.Title);
                    }
                }
            }

            if (node == null)
                return null;
                
            // update height and rebalance
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance = GetBalance(node);

            // LL
            if (balance > 1 && GetBalance(node.Left!) >= 0)
                return RightRotate(node);

            // LR
            if (balance > 1 && GetBalance(node.Left!) < 0)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }

            // RR
            if (balance < -1 && GetBalance(node.Right!) <= 0)
                return LeftRotate(node);

            // RL
            if (balance < -1 && GetBalance(node.Right!) > 0)
            {
                node.Right = RightRotate(node.Right!);
                return LeftRotate(node);
            }

            return node;
        }

        // find leftmost node
        private q1_AVLNode MinValueNode(q1_AVLNode node)
        {
            q1_AVLNode current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        private int GetHeight(q1_AVLNode? node)
        {
            return node?.Height ?? 0;
        }

        // balance factor
        private int GetBalance(q1_AVLNode? node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        // rotate right
        private q1_AVLNode RightRotate(q1_AVLNode y)
        {
            q1_AVLNode x = y.Left!;
            q1_AVLNode? T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            // heights need updating
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

            return x;
        }

        // rotate left
        private q1_AVLNode LeftRotate(q1_AVLNode x)
        {
            q1_AVLNode y = x.Right!;
            q1_AVLNode? T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            return y;
        }
    }
}
