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

        // in-order traversal
        private IEnumerable<q1_Book> InOrderNodes(q1_AVLNode? node)
        {
            if (node == null)
                yield break;

            // left
            foreach (var b in InOrderNodes(node.Left))
                yield return b;

            yield return node.Data;

            // right
            foreach (var b in InOrderNodes(node.Right))
                yield return b;
        }
        
        private q1_AVLNode InsertNode(q1_AVLNode? node, q1_Book book)
        {
            // base case - empty spot found
            if (node == null)
                return new q1_AVLNode(book);

            // BST insert logic
            if (book.Year < node.Data.Year)
                node.Left = InsertNode(node.Left, book);
            else if (book.Year > node.Data.Year)
                node.Right = InsertNode(node.Right, book);
            else
            {
                // same year, compare titles
                if (string.Compare(book.Title, node.Data.Title) < 0)
                    node.Left = InsertNode(node.Left, book);
                else
                    node.Right = InsertNode(node.Right, book);
            }

            // update height
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // get balance factor
            int balance = GetBalance(node);

            // LL case - right rotate
            if (balance > 1 && book.Year < node.Left!.Data.Year)
                return RightRotate(node);

            // RR case - left rotate
            if (balance < -1 && book.Year > node.Right!.Data.Year)
                return LeftRotate(node);

            // LR case - left then right
            if (balance > 1 && book.Year > node.Left!.Data.Year)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }

            // RL case - right then left
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

            // navigate to the node
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
                // year matches, check title
                int titleCmp = string.Compare(title, node.Data.Title);
                if (titleCmp < 0)
                    node.Left = DeleteNode(node.Left, year, title);
                else if (titleCmp > 0)
                    node.Right = DeleteNode(node.Right, year, title);
                else
                {
                    // found the node to delete
                    
                    // case 1: node has one child or no children
                    if (node.Left == null || node.Right == null)
                    {
                        node = node.Left ?? node.Right;
                    }
                    else
                    {
                        // case 2: node has two children
                        // find minimum in right subtree (successor)
                        q1_AVLNode successor = MinValueNode(node.Right!);
                        node.Data = successor.Data;
                        node.Right = DeleteNode(node.Right, successor.Data.Year, successor.Data.Title);
                    }
                }
            }

            if (node == null)
                return null;
                
            // update height and balance
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance = GetBalance(node);

            // LL case
            if (balance > 1 && GetBalance(node.Left!) >= 0)
                return RightRotate(node);

            // LR case
            if (balance > 1 && GetBalance(node.Left!) < 0)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }

            // RR case
            if (balance < -1 && GetBalance(node.Right!) <= 0)
                return LeftRotate(node);

            // RL case
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
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        private int GetHeight(q1_AVLNode? node)
        {
            return node?.Height ?? 0;
        }

        private int GetBalance(q1_AVLNode? node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }

        private q1_AVLNode RightRotate(q1_AVLNode y)
        {
            q1_AVLNode x = y.Left!;
            q1_AVLNode? T2 = x.Right;

            // rotate
            x.Right = y;
            y.Left = T2;

            // update heights
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

            return x;
        }

        private q1_AVLNode LeftRotate(q1_AVLNode x)
        {
            q1_AVLNode y = x.Right!;
            q1_AVLNode? T2 = y.Left;

            // rotate
            y.Left = x;
            x.Right = T2;

            // update heights
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            return y;
        }
    }
}
