# Dstruct

Dstruct is an efficient data structures library written for .NET. It includes alternative implementations of all the common data structures included in the standard library. Dstruct was primarily created as a way for me to better understand commonly used algorithms and data structures.

Includes all the data structures specified below:

### Graphs
```c#
var graph = new ListGraph<int>();
graph.AddVertex(1);
graph.AddVertex(2);
graph.AddVertex(3);
graph.AddVertex(4);
graph.AddVertex(5);

graph.AddDirectedEdge(1, 2);
graph.AddDirectedEdge(1, 3);
graph.AddDirectedEdge(1, 4);

graph.AddUndirectedEdge(3, 5);
graph.AddDirectedEdge(3, 4);

var neighbors1 = graph.Neighbors(1); // [2, 3, 4]
var neighbors3 = graph.Neighbors(3); // [1, 5, 3]
var neighbors5 = graph.Neighbors(5); // []
```

### Lists
LinkedList, ArrayList, and sorting utilities

```c#
var list = new ArrayList<int>();
list.PushFront(1);
list.PushBack(2);
list.PushBack(4); // [1, 2, 4]

list.Insert(1, 3); // [1, 2, 3, 4]

list.Remove(1); // [1, 3, 4]

new Sorter<int>(SortType.Desc).MergeSort(arr); // [4, 3, 1]

var elem = list[1]; // 3
```

### Stack
Linked stack, and stack utilities

```c#
var stack = new Stack<int>();
stack.PushFront(6);
stack.PushFront(5);
stack.PushFront(4);

var elem1 = stack.Pop(); // 4
var elem2 = stack.Pop(); // 5
var elem3 = stack.Pop(); // 6
```

### PriorityQueue

```c#
var heap = new Heap<int>(PriorityType.Min);
heap.Push(5);
heap.Push(10);
heap.Push(2);
heap.Push(7);
 
heap.Pop(); // 2
heap.Pop(); // 5
heap.Pop(); // 7
heap.Pop(); // 10
```

### Trees
BinaryTree and AVLTree
```c#
var tree = new AvlTree<int, char>();
tree.Put(20, 'A');
tree.Put(4, 'B');
tree.Put(26, 'C');
tree.Put(3, 'D');
tree.Put(9, 'E');
tree.Put(2, 'F');
tree.Put(7, 'G');
tree[11] = 'H';
tree[21] = 'I';

var elem1 = tree.Get(20); // 'A'
var elem2 = tree[11]; // 'H'

var elements = tree.Elements(); // ['F', 'D', 'B', 'G', 'E', 'H', 'A', 'I', 'C']
var rangeElems = tree.RangeSearch(2, 7); // ['F', 'D', 'G']
```

### HashTable
```c# 
var table = new HashTable<int, char>(25);
table.Put(1, 'A');
table[10] = 'B';

var elem1 = table.Get(1); // 'A'
var elem2 = table[10]; // 'B'
var elem3 = table[5]; // '\0' (the zero value of the type)

var exists = table.Contains(5); // false
```