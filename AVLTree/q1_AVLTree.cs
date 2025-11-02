namespace AVLTree
{
    // q1_ AVL tree implementation storing q1_Book nodes, keyed by Year then Title
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

        private IEnumerable<q1_Book> InOrderNodes(q1_AVLNode? node)
        {
            if (node == null)
                yield break;

            foreach (var b in InOrderNodes(node.Left))
                yield return b;

            yield return node.Data;

            foreach (var b in InOrderNodes(node.Right))
                yield return b;
        }

        private q1_AVLNode InsertNode(q1_AVLNode? node, q1_Book book)
        {
            if (node == null)
                return new q1_AVLNode(book);

            // BST insert: primary key Year, secondary Title for deterministic placement
            if (book.Year < node.Data.Year)
                node.Left = InsertNode(node.Left, book);
            else if (book.Year > node.Data.Year)
                node.Right = InsertNode(node.Right, book);
            else
            {
                // same year -> compare title
                int cmp = string.Compare(book.Title, node.Data.Title, StringComparison.Ordinal);
                if (cmp < 0)
                    node.Left = InsertNode(node.Left, book);
                else
                    node.Right = InsertNode(node.Right, book);
            }

            // update height
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // balance
            int balance = GetBalance(node);

            // Left Left
            if (balance > 1 && book.Year < node.Left!.Data.Year)
                return RightRotate(node);

            // Right Right
            if (balance < -1 && book.Year > node.Right!.Data.Year)
                return LeftRotate(node);

            // Left Right
            if (balance > 1 && book.Year > node.Left!.Data.Year)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }

            // Right Left
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
                // same year - need to compare title to find exact node
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
                    // this node should be deleted
                    // node with only one child or no child
                    if (node.Left == null || node.Right == null)
                    {
                        q1_AVLNode? temp = node.Left ?? node.Right;

                        // no child
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
                        // node with two children: get inorder successor (smallest in right subtree)
                        q1_AVLNode temp = MinValueNode(node.Right!);

                        // copy the inorder successor's data to this node
                        node.Data = temp.Data;

                        // delete the inorder successor
                        node.Right = DeleteNode(node.Right, temp.Data.Year, temp.Data.Title);
                    }
                }
            }

            // if the tree had only one node then return
            if (node == null)
                return null;

            // UPDATE HEIGHT
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            int balance = GetBalance(node);

            // Left Left
            if (balance > 1 && GetBalance(node.Left!) >= 0)
                return RightRotate(node);

            // Left Right
            if (balance > 1 && GetBalance(node.Left!) < 0)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }

            // Right Right
            if (balance < -1 && GetBalance(node.Right!) <= 0)
                return LeftRotate(node);

            // Right Left
            if (balance < -1 && GetBalance(node.Right!) > 0)
            {
                node.Right = RightRotate(node.Right!);
                return LeftRotate(node);
            }

            return node;
        }

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

        private int GetBalance(q1_AVLNode? node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        private q1_AVLNode RightRotate(q1_AVLNode y)
        {
            q1_AVLNode x = y.Left!;
            q1_AVLNode? T2 = x.Right;

            // rotation
            x.Right = y;
            y.Left = T2;

            // update heights
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

            // new root
            return x;
        }

        private q1_AVLNode LeftRotate(q1_AVLNode x)
        {
            q1_AVLNode y = x.Right!;
            q1_AVLNode? T2 = y.Left;

            // rotation
            y.Left = x;
            x.Right = T2;

            // update heights
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            // new root
            return y;
        }
    }
}
