namespace AVLTree
{
    
    public class q1_AVLTree // q1_ AVL tree implementation storing q1_Book nodes, keyed by Year then Title
    {
        
        private q1_AVLNode? Root; // Root node of the AVL tree

        
        public q1_AVLTree() // Constructor - initializes an empty AVL tree
        {
            Root = null;
        }

        public void Insert(q1_Book book) 
        {
            Root = InsertNode(Root, book);
        }

        
        public void Delete(int year, string title) // Public method to delete a book by year and title
        {
            Root = DeleteNode(Root, year, title);
        }

        
        public IEnumerable<q1_Book> InOrder() // Public method to get all books in sorted order (in-order traversal)
        {
            return InOrderNodes(Root);
        }


        private IEnumerable<q1_Book> InOrderNodes(q1_AVLNode? node)
        // Returns books sorted by year, then by title
        {

            if (node == null) // Base case: empty subtree
                yield break;

            foreach (var b in InOrderNodes(node.Left)) // Recursively traverse left subtree (smaller years/titles)
                yield return b;

            yield return node.Data; // Visit current node

            foreach (var b in InOrderNodes(node.Right)) // Recursively traverse right subtree (larger years/titles)
                yield return b;
        }
        
        private q1_AVLNode InsertNode(q1_AVLNode? node, q1_Book book)  // Recursive method to insert a book node into the AVL tree
        {
            
            if (node == null)
                return new q1_AVLNode(book); // Base case: found the correct position, create new leaf node

            // BST insert: primary key Year, secondary Title for deterministic placement
            // Compare by year first
            if (book.Year < node.Data.Year)
                node.Left = InsertNode(node.Left, book);
            else if (book.Year > node.Data.Year)
                node.Right = InsertNode(node.Right, book);
            else
            {
                // Same year -> compare by title alphabetically
                int cmp = string.Compare(book.Title, node.Data.Title, StringComparison.Ordinal);
                if (cmp < 0)
                    node.Left = InsertNode(node.Left, book);
                else
                    node.Right = InsertNode(node.Right, book);
            }

            // Update height of current node after insertion
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

            // Calculate balance factor to check if rebalancing is needed
            int balance = GetBalance(node);

            // Fix with single right rotation
            if (balance > 1 && book.Year < node.Left!.Data.Year)
                return RightRotate(node);

            // Fix with single left rotation
            if (balance < -1 && book.Year > node.Right!.Data.Year)
                return LeftRotate(node);

            // Fix with double rotation: left rotate child, then right rotate parent
            if (balance > 1 && book.Year > node.Left!.Data.Year)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }

            // Fix with double rotation: right rotate child, then left rotate parent
            if (balance < -1 && book.Year < node.Right!.Data.Year)
            {
                node.Right = RightRotate(node.Right!);
                return LeftRotate(node);
            }

            // Tree is balanced, return unchanged node
            return node;
        }

        // Recursive method to delete a node from the AVL tree
        private q1_AVLNode? DeleteNode(q1_AVLNode? node, int year, string title)
        {
            // Base case: node not found
            if (node == null)
                return null;

            // Step 1: Perform standard BST delete - navigate to the target node
            if (year < node.Data.Year)
            {
                // Target is in left subtree
                node.Left = DeleteNode(node.Left, year, title);
            }
            else if (year > node.Data.Year)
            {
                // Target is in right subtree
                node.Right = DeleteNode(node.Right, year, title);
            }
            else
            {
                // Same year - compare titles to find exact node
                int cmp = string.Compare(title, node.Data.Title, StringComparison.Ordinal);
                if (cmp < 0)
                {
                    // Target title comes before current node's title
                    node.Left = DeleteNode(node.Left, year, title);
                }
                else if (cmp > 0)
                {
                    // Target title comes after current node's title
                    node.Right = DeleteNode(node.Right, year, title);
                }
                else
                {
                    // Found the node to delete
                    if (node.Left == null || node.Right == null)
                    {
                        // Get the non-null child (or null if no children)
                        q1_AVLNode? temp = node.Left ?? node.Right;

                        // No children - remove this node
                        if (temp == null)
                        {
                            node = null;
                        }
                        else
                        {
                            // One child - replace current node with child
                            node = temp;
                        }
                    }
                    else
                    {
                        // Get inorder successor 
                        q1_AVLNode temp = MinValueNode(node.Right!);

                        // Copy the inorder successor's data to this node
                        node.Data = temp.Data;

                        // Delete the inorder successor
                        node.Right = DeleteNode(node.Right, temp.Data.Year, temp.Data.Title);
                    }
                }
            }

            // If the tree had only one node, deletion is complete
            if (node == null)
                return null;
                
            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            int balance = GetBalance(node);

            // Fix with single right rotation
            if (balance > 1 && GetBalance(node.Left!) >= 0)
                return RightRotate(node);

            // Fix with double rotation: left rotate child, then right rotate parent
            if (balance > 1 && GetBalance(node.Left!) < 0)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }

            // Fix with single left rotation
            if (balance < -1 && GetBalance(node.Right!) <= 0)
                return LeftRotate(node);

            // Fix with double rotation: right rotate child, then left rotate parent
            if (balance < -1 && GetBalance(node.Right!) > 0)
            {
                node.Right = RightRotate(node.Right!);
                return LeftRotate(node);
            }

            // Tree is balanced, return node
            return node;
        }

       
        private q1_AVLNode MinValueNode(q1_AVLNode node)  // Used to find inorder successor during deletion
        {
            q1_AVLNode current = node;
            // Keep going left until no more left children
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        // Get the height of a node (0 if null)
        private int GetHeight(q1_AVLNode? node)
        {
            return node?.Height ?? 0;
        }
        private int GetBalance(q1_AVLNode? node) // Balance factor = height(left subtree) - height(right subtree)
        {
            if (node == null)
                return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        
        private q1_AVLNode RightRotate(q1_AVLNode y) // Right rotation to fix left-heavy imbalance
        {
            // Store references
            q1_AVLNode x = y.Left!;
            q1_AVLNode? T2 = x.Right;

            // Perform rotation
            x.Right = y;
            y.Left = T2;

            // Update heights (y first since it's now below x)
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));

            // Return new root of this subtree
            return x;
        }

        // Left rotation to fix right-heavy imbalance
        private q1_AVLNode LeftRotate(q1_AVLNode x)
        {
            // Store references
            q1_AVLNode y = x.Right!;
            q1_AVLNode? T2 = y.Left;

            // Perform rotation
            y.Left = x;
            x.Right = T2;

            // Update heights (x first since it's now below y)
            x.Height = 1 + Math.Max(GetHeight(x.Left), GetHeight(x.Right));
            y.Height = 1 + Math.Max(GetHeight(y.Left), GetHeight(y.Right));

            // Return new root of this subtree
            return y;
        }
    }
}
