# Treap: Randomized Binary Search Tree Implementation in C#

**Project Title:** Treap Data Structure Implementation

**Technology:** C#, .NET Framework, Console Application

**Overview:** This project implements a Treap (Tree + Heap) data structure in C# that combines the properties of both binary search trees and heaps. It maintains binary search tree ordering by keys while using random priorities to maintain heap properties, ensuring balanced tree operations with expected O(log n) performance.

**Key Features:** This project showcases advanced data structure implementation skills using C#. It utilizes a randomized approach to maintain tree balance through priority-based heap properties. The implementation demonstrates proficiency in tree rotations, recursive algorithms, and complex data structure operations including split and merge functionality.

**Learning Outcomes:** Mastery in C# programming and advanced tree data structure implementation. Proficiency in randomized algorithms and tree balancing techniques. Skill in recursive programming and algorithm optimization with logarithmic time complexity analysis.

**Purpose:** This project was developed to demonstrate expertise in implementing complex probabilistic data structures. It reflects the ability to combine theoretical computer science concepts with practical implementations for efficient set operations and range queries.

---

## Implementation

The primary data structure used is a **Treap**. This is a randomized binary search tree that maintains both BST property by keys and heap property by random priorities. The code consists of the TreapNode and Treap classes which implement: Insert, Delete, Search, Split, Merge, and RangeQuery operations.

### TreapNode Class:
**Description:** Represents a node in the Treap, containing a key, random priority, and left/right child pointers.

**Constructor:** Initializes a new node with the given key and assigns a random priority value.

**Time Complexity:** O(1) for initialization.

### Methods for Treap:

**1. Insert:** This method inserts a key into the treap  
**Input:** int key  
**Operation:** Recursively inserts the key while maintaining BST and heap properties through rotations  
**Time Complexity:** O(log n)

**2. Delete(int key):** Removes a key from the treap  
**Input:** int key  
**Operation:** Finds and removes the specified key while maintaining treap properties through rotations  
**Time Complexity:** O(log n)

**3. Search(int key):** Searches for a key in the treap  
**Input:** int key  
**Operation:** Traverses the treap using BST property to find the key  
**Time Complexity:** O(log n)

**4. Split(int key):** Splits the treap into two treaps  
**Input:** int key  
**Operation:** Divides the treap into left treap (keys < key) and right treap (keys ≥ key)  
**Time Complexity:** O(log n)

**5. Merge(Treap, Treap):** Merges two treaps into one  
**Input:** Two treap objects  
**Operation:** Combines two treaps while maintaining heap property based on priorities  
**Time Complexity:** O(log n)

**6. RangeQuery(int low, int high):** Finds all keys within a range  
**Input:** int low, int high  
**Operation:** Traverses the treap and prints all keys within the specified range  
**Time Complexity:** O(log n + k) where k is the number of keys in range
