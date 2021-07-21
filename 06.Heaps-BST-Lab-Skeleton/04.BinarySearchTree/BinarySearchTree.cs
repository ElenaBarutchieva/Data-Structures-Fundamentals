namespace _04.BinarySearchTree
{
    using System;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Root = root;
        }

        public Node<T> Root { get; private set; }

        public T Value { get { return this.Root.Value; } }

        public Node<T> LeftChild { get; set; }

        public Node<T> RightChild { get; set; }

        public bool Contains(T element)
        {
            if (this.Search(element) == null)
            {
                return false;
            }
            return true;
        }


        public void Insert(T element)
        {
            this.InsertRecursion(element, Root);
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            return this.SearchRecursion(element, Root);
        }

        private IAbstractBinarySearchTree<T> SearchRecursion(T element, Node<T> currentElement)
        {
            if (currentElement == null)
            {
                return null;
            }
            if (currentElement.Value.CompareTo(element) == 0)
            {
                return new BinarySearchTree<T>(new Node<T>(element,currentElement.LeftChild, currentElement.RightChild));
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

        private void InsertRecursion(T element, Node<T> currentElement)
        {
            if (currentElement == null)
            {
                currentElement = new Node<T>(element, null, null);
                this.Root = currentElement;
            }
            if (currentElement.Value.CompareTo(element) > 0)
            {
                if (currentElement.LeftChild == null)
                {
                    currentElement.LeftChild = new Node<T>(element, null, null);
                    return;
                }
                InsertRecursion(element, currentElement.LeftChild);
            }
            else if(currentElement.Value.CompareTo(element) < 0)
            {
                if (currentElement.RightChild == null)
                {
                    currentElement.RightChild = new Node<T>(element, null, null);
                    return;
                }
                InsertRecursion(element, currentElement.RightChild);
            }
        }
    }
}
