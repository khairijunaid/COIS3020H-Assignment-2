using System;

class TreapNode
{
    public int Key;
    public int Priority;
    public TreapNode Left;
    public TreapNode Right;

    public TreapNode(int key)
    {
        Key = key;
        Priority = new Random().Next();
    }
}

class Treap
{
    private TreapNode root;

    public void Insert(int key)
    {
        root = InsertNode(root, key);
    }
    // InsertNode
    // This method inserts key into treap
    // O(log n) because each iteration cuts tree in half
    private TreapNode InsertNode(TreapNode node, int key)
    {
        // If the current node is null, create a new node with the given key
        if (node == null)
            return new TreapNode(key);

        // Handling duplicate keys
        if (key == node.Key)
        {
            return node;
        }
        // Recursive insertion based on the comparison of keys
        if (key < node.Key)
        {
            // Traverse left and insert the key
            node.Left = InsertNode(node.Left, key);

            // Check and perform rotation if necessary to maintain heap property
            if (node.Left.Priority > node.Priority)
                node = RotateRight(node);
        }
        else
        {
            // Traverse right and insert the key
            node.Right = InsertNode(node.Right, key);

            // Check and perform rotation if necessary to maintain heap property
            if (node.Right.Priority > node.Priority)
                node = RotateLeft(node);
        }

        return node;
    }

    // RotateRight
    // Rotates treap right
    // O(1) because node pointer is given and only needs to rotate between given pointer
    private TreapNode RotateRight(TreapNode node)
    {
        TreapNode newRoot = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;
        return newRoot;
    }

    // RotateLeft
    // Rotates treap left
    // O(1) because node pointer is given and only needs to rotate between given pointer
    private TreapNode RotateLeft(TreapNode node)
    {
        TreapNode newRoot = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;
        return newRoot;
    }

    public void Delete(int key)
    {
        root = DeleteNode(root, key);
    }

    // DeleteNode
    // Deletes treap node based on key provided
    // O(log n) because each iteration cuts tree in half
    private TreapNode DeleteNode(TreapNode node, int key)
    {
        // Handling cases where the node is null or the key is not found
        if (node == null)
            return node;

        // Finding the node to delete based on the key
        if (key < node.Key)
            node.Left = DeleteNode(node.Left, key);
        else if (key > node.Key)
            node.Right = DeleteNode(node.Right, key);
        else
        {
            // Cases for node deletion based on its children and priorities
            if (node.Left == null)
                return node.Right;
            else if (node.Right == null)
                return node.Left;

            if (node.Left.Priority > node.Right.Priority)
            {
                node = RotateRight(node);
                node.Right = DeleteNode(node.Right, key);
            }
            else
            {
                node = RotateLeft(node);
                node.Left = DeleteNode(node.Left, key);
            }
        }
        return node;
    }

    public bool Search(int key)
    {
        return SearchNode(root, key);
    }

    // SearchNode
    // Searches for node based on key provided
    // O(log n) because each iteration cuts tree in half
    private bool SearchNode(TreapNode node, int key)
    {
        // Handling cases where the node is null or the key is not found
        if (node == null)
            return false;

        // Recursive search based on the comparison of keys
        if (key == node.Key)
            return true;
        else if (key < node.Key)
            return SearchNode(node.Left, key);
        else
            return SearchNode(node.Right, key);
    }

    public (Treap, Treap) Split(int key)
    {
        var (left, right) = SplitNode(root, key);
        return (new Treap { root = left }, new Treap { root = right });
    }

    private (TreapNode, TreapNode) SplitNode(TreapNode node, int key)
    {
        // Handling cases where the node is null
        if (node == null)
            return (null, null);

        // Splitting the Treap based on the comparison of keys
        if (key < node.Key)
        {
            var (left, right) = SplitNode(node.Left, key);
            node.Left = right;
            return (left, node);
        }
        else
        {
            var (left, right) = SplitNode(node.Right, key);
            node.Right = left;
            return (node, right);
        }
    }

    public static Treap Merge(Treap treap1, Treap treap2)
    {
        var merged = new Treap();
        merged.root = MergeNodes(treap1.root, treap2.root);
        return merged;
    }

    //MergeNodes
    //Merges 2 treaps from the treaps given in Merge()
    //O(log n) each iteration cuts tree in half
    private static TreapNode MergeNodes(TreapNode node1, TreapNode node2)
    {
        // Handling cases where one of the nodes is null
        if (node1 == null)
            return node2;
        if (node2 == null)
            return node1;

        // Merging two Treaps based on priorities
        if (node1.Priority > node2.Priority)
        {
            node1.Right = MergeNodes(node1.Right, node2);
            return node1;
        }
        else
        {
            node2.Left = MergeNodes(node1, node2.Left);
            return node2;
        }
    }

    public void RangeQuery(int low, int high)
    {
        RangeQuery(root, low, high);
    }

    //RangeQuery
    //Prints all node keys within given range
    //O(log n + k) where n is total nodes and k is the number of nodes that fall in the range 
    private void RangeQuery(TreapNode node, int low, int high)
    {
        // Handling cases where the node is null
        if (node == null)
            return;

        // Recursive traversal and printing of keys within the specified range
        if (node.Key < low)
            RangeQuery(node.Right, low, high);
        else if (node.Key > high)
            RangeQuery(node.Left, low, high);
        else
        {
            Console.WriteLine(node.Key);
            RangeQuery(node.Left, low, high);
            RangeQuery(node.Right, low, high);
        }
    }
}

class Program
{
    public static void Main()
    {
        // Create a Treap
        Treap treap = new Treap();

        // Insertion
        treap.Insert(5);
        treap.Insert(3);
        treap.Insert(8);
        treap.Insert(1);
        treap.Insert(7);
        treap.Insert(10);
        treap.Insert(15);
        Console.WriteLine("Inserted elements: 5, 3, 8, 1, 7, 10, 15");
        treap.RangeQuery(int.MinValue, int.MaxValue);

        // Edge Case: Inserting the same element multiple times
        Console.WriteLine("Edge Case: Inserting the same element (5) multiple times:");
        treap.Insert(5);
        treap.Insert(5);
        treap.Insert(5);
        Console.WriteLine("After inserting 5 four times:");
        treap.RangeQuery(int.MinValue, int.MaxValue); // Expected: 5 (should have only one 5)


        // Search
        Console.WriteLine("Search 3: " + treap.Search(3)); // Expected: True
        Console.WriteLine("Search 4: " + treap.Search(4)); // Expected: False
        Console.WriteLine();

        // Deletion
        treap.Delete(3);
        Console.WriteLine("After deleting 3:");
        Console.WriteLine("Search 3: " + treap.Search(3)); // Expected: False
        Console.WriteLine();

        // Edge Case: Deleting a non-existent element
        Console.WriteLine("\nEdge Case: Deleting a non-existent element (2):");
        treap.Delete(2);
        Console.WriteLine("After attempting to delete 2:");
        treap.RangeQuery(int.MinValue, int.MaxValue); // Expected: 10, 5, 1, 8, 7, 15

        // Split
        var (left, right) = treap.Split(7);
        Console.WriteLine("After splitting at 7:");
        Console.WriteLine("Split Left:");
        left.RangeQuery(int.MinValue, int.MaxValue); // Expected: 5, 1, 7
        Console.WriteLine("Split Right:");
        right.RangeQuery(int.MinValue, int.MaxValue); // Expected: 8, 10, 15
        Console.WriteLine();

        // Split Empty Treap
        Console.WriteLine("Splitting Empty Treap: ");
        Treap emptyTreap = new Treap();
        treap.Split(5);
        Console.WriteLine();

        // Merge
        var merged = Treap.Merge(left, right);
        Console.WriteLine("After merging the split Treaps:");
        Console.WriteLine("Merged Treap:");
        merged.RangeQuery(int.MinValue, int.MaxValue); // Expected: 1, 5, 7, 8, 10, 15
        Console.WriteLine();

        // Edge Case: Merging an empty treap
        var mergedEmpty = Treap.Merge(emptyTreap, left);
        Console.WriteLine("\nEdge Case: Merging with an empty Treap:");
        Console.WriteLine("Merged Treap with an empty Treap:");
        mergedEmpty.RangeQuery(int.MinValue, int.MaxValue); // Expected: 5, 1, 7


        // Additional Test Cases
        Treap additionalTreap = new Treap();
        additionalTreap.Insert(20);
        additionalTreap.Insert(6);
        additionalTreap.Insert(12);
        Console.WriteLine("Inserted elements into additional Treap: 20, 6, 12");

        // Merge with the additional Treap
        var finalMerged = Treap.Merge(merged, additionalTreap);
        Console.WriteLine("After merging with additional Treap:");
        Console.WriteLine("Final Merged Treap:");
        finalMerged.RangeQuery(int.MinValue, int.MaxValue); // Expected: 1, 5, 6, 7, 8, 10, 12, 15, 20

        //RangeQuery Test
        Console.WriteLine();
        Console.WriteLine("RangedQuery 6 to 12:");
        finalMerged.RangeQuery(6, 12); //Expected: 6,7,8,10,12
    }
}