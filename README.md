# DataStructure
A project for data structure and algorithm analysis practice

### Catalog
Note that the catalog is in chronological order, and due to my incompetency some data structure or algorithms are beyond my ability, so I may leave them unfinished. I shall be trying my best, but I cannot make no promise. :)

- RedBlackTree<TK, TV> a red-black tree implementation
	- One issue I did not figure out is the concatenation operation: Given two red-black trees _A_ and _B_ and a new node c, simultaneously assuring that foreach _a_ in _A_ and _b_ in _B_, a.key<c.key<b.key. The output is a new red-black tree containing all _a_, _b_ and c nodes in theta(lg(n)) time.
- AVLTree **Not finished yet** a AVL-Tree implementation, also known as a height-balanced tree
- VEBTree<TK, TV> a van Emde Boas tree implementation
- Basic Contains some base classes and interfaces that data structures may implement
	- IBinaryTree<TNode, TK> Indicates this class can be used as a binary tree
	- IHeap<TNode, TK> Indicates this class can be used as a heap
	- IPriorityQueue<TNode> Indicates this class can be used as a priority queue
- Treap<TK, TP> a treap implementation
- BTree **Not finished yet** a B-Tree implementation. May later extend to B+Tree I hope
- FibonacciHeap **Not finished yet** a Fibonacci heap implementation


### Notice
This project is created and maintained when the author is learning data structures and algorithm analysis in his leisure time. Since C++-based data structure has already been predominated by algorithm aficionados, I decided to use a different language to implement the same algorithms. So on the one hand, this is an addition to the collection of data structures or algorithms written in divers programming languages, and on the other hand, I do no intend to copy existing codes nor spuriously call them original.
The project is open-source and free (obviously), and you may send me pull requests or contact me through my email (2511146542@qq.com) if you find some bug or want to contribute to the C# data structures and algorithms implementation.
Thanks for you time any way.


### Version changes
- 4.5
	- Add B-Tree, Fibonacci Heap. Implemented but yet not verified.
	- No longer throws an ArgumentNullException when using AVLNode.Equals(object), RedBlackNode.Equals(object), and TreapNode.Equals(object) when the input parameter obj (type object) is null.
	- Treap<TK,TP> now implements IHeap<TreapNode<TK,TP>>, IBinaryTree<TreapNode<TK,TP>, TK> and IPriorityQueue<TreapNode<TK,TP>>, so a treap now can be used as a heap, binary tree or priority queue.
- 4.6
	- Trying to Fix Fibonacci Heap, but I failed. I fixed something, I know, but the ExtractMin() still does not work. After removing the minimum root list, the root list went wrong.