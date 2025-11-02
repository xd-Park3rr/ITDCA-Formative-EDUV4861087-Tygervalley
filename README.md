# AVLTree (Question 1 & 2)

This folder contains two related implementations of an AVL (self-balancing binary search) tree used to store book listings.

Contents
- `Program.cs` — demo program that shows both Q1 and Q2 features.
- `q1_AVLTree.cs` — original AVL implementation for Question 1 (classes prefixed with `q1_`). This file implements the tree, node rotations, insert/delete, and an `InOrder()` method that yields `q1_Book` instances.
- `properties/q1_Book.cs` and `properties/q1_AVLNode.cs` — data model and node type used by the q1 implementation.
- `q2_AVLTree.cs` — Question 2 wrapper that builds on top of the q1 implementation and adds extra features. Classes for Q2 are prefixed with `q2_` and are intentionally separate.
- `properties/q2_Book.cs` and `properties/q2_AVLNode.cs` — lightweight Q2 models mirroring Q1 where needed.

High-level design
---------------

1) q1 (Question 1)

- The q1 implementation is a standard AVL tree storing `q1_Book` objects. Nodes (`q1_AVLNode`) hold a single book in `Data`, pointers to `Left` and `Right` children, and an integer `Height`.
- Ordering (BST property): nodes are ordered primarily by `Year` (older -> left, newer -> right). If two books have the same `Year` a secondary comparison by `Title` (ordinal string compare) is used as a deterministic tiebreaker.
- Insert and Delete are implemented recursively. After modifying a subtree, the node's `Height` is updated and the node is rebalanced if its balance factor (height(left) - height(right)) is outside [-1, 1].
- Rotations implemented: LeftRotate and RightRotate. Double rotations (LR and RL) are implemented by composing single rotations.

2) q2 (Question 2)

- The q2 implementation intentionally reuses the q1 AVL code rather than duplicating it. The goal is to add features on top of the working, tested AVL logic while keeping q1 files untouched (per your request).
- `q2_AVLTree` is a wrapper that delegates storage and balancing to an internal `q1_AVLTree` instance. It converts `q2_Book` to `q1_Book` for insertion and relies on `q1_AVLTree` for maintenance.
- To implement binary search and right-most traversal without changing q1 source code, `q2_AVLTree` reads the private `_root` field of `q1_AVLTree` using reflection at runtime. It traverses `q1_AVLNode` nodes directly for operations that benefit from direct binary-tree access (SearchByYear and GetMostRecentBook). This preserves O(log n) behavior.

Key operations explained
----------------------

Nodes and Height
- A node stores Data (a Book), Left and Right child references, and Height (leaf height = 1). Height is used to compute balance factor = height(left) - height(right).

Insertion (brief)
- Follow normal BST insertion comparing Year (then Title). Create a new node when reaching null.
- On the way back up recursion, update node.Height and compute balance.
- If balance is > 1 or < -1, perform appropriate rotation(s):
  - Left-Left (LL): RightRotate
  - Right-Right (RR): LeftRotate
  - Left-Right (LR): LeftRotate(left), RightRotate(node)
  - Right-Left (RL): RightRotate(right), LeftRotate(node)

Deletion (brief)
- Find the node by Year and Title (to disambiguate same-year items). If node has 0 or 1 child, replace it with its child (or null). If it has 2 children, copy the inorder successor (minimum in the right subtree) into the node, then delete the successor.
- After deletion unwind, update heights and rebalance as in insertion.

Search by Year (binary search)
- The tree's BST ordering by Year allows efficient search. Start at root; compare the requested year with the node's year and go left or right accordingly until found or reaching null. This is O(h) where h is the tree height (O(log n) for AVL).

Most recent book
- The most recent book is the right-most node in the BST (keep going `Right` until null). This is O(h).

In-order traversal
- Recursively visit Left, then Node, then Right. For q1 the `InOrder()` yields complete `q1_Book` objects; q2 provides `InOrderTitles()` which maps to titles only.

Counting items
- q1 does not maintain a public Count. The q2 wrapper maintains its own `Count` updated on inserts and deletes so retrieving total books is O(1) for q2.

Why the wrapper/reflection approach?
- You requested q1 code remain untouched. To avoid duplicating AVL logic while q2 must "take advantage" of the existing AVL structure (i.e., perform binary search using the tree), the wrapper reads q1's private Root using reflection. This keeps behavior consistent and avoids code duplication while meeting the requirement.

What the demo shows
- The program first runs the q1 demo inserting sample books and printing an in-order listing.
- It then deletes two books and prints the in-order listing again.
- After that, the q2 demo constructs a q2_AVLTree from the same sample data and demonstrates:
  - `InOrderTitles()` — prints just titles in ascending order.
  - `SearchByYear(year)` — binary search for a given year.
  - `GetMostRecentBook()` — shows the title and year of the newest book.
  - `Count` — total books in the q2 tree.

---

## PriorityQueue (Question 3)

Located in the `PriorityQueue` folder is a small program that implements a simple
patient priority queue for a clinic (classes prefixed with `q3_`). The queue
uses priorities 0 (most urgent) through 4 (least urgent). When a patient is
dequeued their name is printed to the console.

Usage: run the `PriorityQueue` project and provide commands on stdin. Supported
commands (one per line):

- `ENQUEUE <name> <priority>` — add a patient with the given numeric priority.
- `DEQUEUE` — remove the next patient (highest priority) and print their name.

Example input:

```
ENQUEUE Alice 2
ENQUEUE Bob 0
DEQUEUE
DEQUEUE
```

The program will print Bob then Alice (Bob has priority 0 which is higher than 2).


