namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.CopyNode(root);
        }

        public Node<T> Root { get; private set; }

        public int Count { get; private set; }

        //Ready
        public bool Contains(T element)
        {
            if (this.Search(element) == null)
            {
                return false;
            }
            return true;
        }
        //Ready
        public IAbstractBinarySearchTree<T> Search(T element)
        {
            return this.SearchRecursion(element, Root);
        }
        //Ready
        public void Insert(T element)
        {
            this.InsertRecursion(element, Root);
            Count++;
        }
        //Ready
        public void EachInOrder(Action<T> action)
        {
            DFSForEach(this.Root, action);
        }
        //Ready
        private void DFSForEach(Node<T> tree, Action<T> action)
        {
            if (tree.LeftChild != null)
            {
                DFSForEach(tree.LeftChild, action);
            }
            action.Invoke(tree.Value);
            if (tree.RightChild != null)
            {
                DFSForEach(tree.RightChild, action);
            }
        }
        //Ready
        public List<T> Range(T lower, T upper)
        {
            var result = new List<T>();
            this.Range(lower, upper, this.Root, result);
            return result;
        }
        //Ready
        private void Range(T start, T end, Node<T> node, List<T> result)
        {
            if (node == null)
            {
                return;
            }
            var inStartRange = start.CompareTo(node.Value);
            var inEndRange = end.CompareTo(node.Value);

            if (inStartRange < 0)
            {
                this.Range(start, end, node.LeftChild, result);
            }

            if (inStartRange <= 0 && inEndRange >= 0)
            {
                result.Add(node.Value);
            }

            if (inEndRange > 0)
            {
                this.Range(start, end, node.RightChild, result);
            }
        }

        public void DeleteMin()
        {
            if (this.IsEmpty())
            {
                throw new InvalidOperationException();
            }

            this.Root.LeftChild = this.DeleteMin(this.Root.LeftChild);
        }

        private Node<T> DeleteMin(Node<T> node)
        {
            if (node.LeftChild == null)
            {
                this.Count--;
                return node.RightChild;
            }
            node.LeftChild = this.DeleteMin(node.LeftChild);
            return node;
        }

        public void DeleteMax()
        {
            if (this.IsEmpty())
            {
                throw new InvalidOperationException();
            }

            this.Root.RightChild = this.DeleteMax(this.Root.RightChild);
        }

        private Node<T> DeleteMax(Node<T> node)
        {
            if (node.RightChild == null)
            {
                this.Count--;
                return node.LeftChild;
            }
            node.RightChild = this.DeleteMin(node.RightChild);
            return node;
        }

        public int GetRank(T element)
        {
            if (this.IsEmpty() || element.CompareTo(default(T)) == 0)
            {
                return 0;
            }
            
            int count = 0;
            this.GetRank(element, ref count, this.Root);
            return count;
        }

        private void GetRank(T element, ref int count, Node<T>node)
        {
            if (node == null)
            {
                return;
            }
            if (node.Value.CompareTo(element) <= 0)
            {
                this.GetRank(element, ref count, node.LeftChild);
            }
            count++;
            if (node.Value.CompareTo(element) < 0)
            {
                this.GetRank(element, ref count, node.RightChild);
            }
        }

        //Ready
        private IAbstractBinarySearchTree<T> SearchRecursion(T element, Node<T> currentElement)
        {
            if (currentElement == null)
            {
                return null;
            }
            if (currentElement.Value.CompareTo(element) == 0)
            {
                return new BinarySearchTree<T>(currentElement);
            }
            if (currentElement.Value.CompareTo(element) > 0)
            {
                return SearchRecursion(element, currentElement.LeftChild);
            }
            else
            {
                return SearchRecursion(element, currentElement.RightChild);
            }
        }
        //Ready
        private void InsertRecursion(T element, Node<T> currentElement)
        {
            if (currentElement == null)
            {
                currentElement = new Node<T>(element);
                this.Root = currentElement;
            }
            if (currentElement.Value.CompareTo(element) > 0)
            {
                if (currentElement.LeftChild == null)
                {
                    currentElement.LeftChild = new Node<T>(element);
                    return;
                }
                InsertRecursion(element, currentElement.LeftChild);
            }
            else if (currentElement.Value.CompareTo(element) < 0)
            {
                if (currentElement.RightChild == null)
                {
                    currentElement.RightChild = new Node<T>(element);
                    return;
                }
                InsertRecursion(element, currentElement.RightChild);
            }
        }
        //Ready
        private bool IsEmpty()
        {
            if (this.Count == 0)
            {
                return true;
            }
            return false;
        }
        //Ready
        private void CopyNode(Node<T> node)
        {
            if (node != null)
            {
                this.Insert(node.Value);
                this.CopyNode(node.LeftChild);
                this.CopyNode(node.RightChild);
            }
        }
    }
}
