namespace _01.BinaryTree
{
    using System;
    using System.Collections.Generic;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
    {
        public BinaryTree(T value
            , IAbstractBinaryTree<T> leftChild
            , IAbstractBinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        public T Value { get; private set; }

        public IAbstractBinaryTree<T> LeftChild { get; private set; }

        public IAbstractBinaryTree<T> RightChild { get; private set; }

        public string AsIndentedPreOrder(int indent)
        {
            return DFSPreOrder(this, indent).Trim();
        }

        private string DFSPreOrder(IAbstractBinaryTree<T> binaryTree, int indent)
        {
            if (binaryTree == null)
            {
                return string.Empty;
            }
            string result = $"{new string(' ', indent)}{binaryTree.Value}\r\n";

            result += DFSPreOrder(binaryTree.LeftChild, indent + 2);
            result += DFSPreOrder(binaryTree.RightChild, indent + 2);

            return result;
        }

        public List<IAbstractBinaryTree<T>> InOrder()
        {
            var list = new List<IAbstractBinaryTree<T>>();
            DFSInOrder(this, list);
            return list;
        }

        private IAbstractBinaryTree<T> DFSInOrder(IAbstractBinaryTree<T> binaryTree, List<IAbstractBinaryTree<T>> list)
        {
            if (binaryTree.LeftChild != null)
            {
                DFSInOrder(binaryTree.LeftChild, list);
            }
            list.Add(binaryTree);
            if (binaryTree.RightChild != null)
            {
                DFSInOrder(binaryTree.RightChild, list);
            }
            return binaryTree;
        }

        public List<IAbstractBinaryTree<T>> PostOrder()
        {
            var list = new List<IAbstractBinaryTree<T>>();
            DFSPostOrder(this, list);
            return list;
        }

        private IAbstractBinaryTree<T> DFSPostOrder(IAbstractBinaryTree<T> binaryTree, List<IAbstractBinaryTree<T>> list)
        {

            if (binaryTree.LeftChild != null)
            {
                DFSPostOrder(binaryTree.LeftChild, list);
            }
            if (binaryTree.RightChild != null)
            {
                DFSPostOrder(binaryTree.RightChild, list);
            }
            list.Add(binaryTree);
            return binaryTree;
        }

        public List<IAbstractBinaryTree<T>> PreOrder()
        {
            var list = new List<IAbstractBinaryTree<T>>();
            DFSPreOrder(this, list);
            return list;
        }

        private IAbstractBinaryTree<T> DFSPreOrder(IAbstractBinaryTree<T> binaryTree, List<IAbstractBinaryTree<T>> list)
        {
            list.Add(binaryTree);
            if (binaryTree.LeftChild != null)
            {
                DFSPreOrder(binaryTree.LeftChild, list);
            }
            if (binaryTree.RightChild != null)
            {
                DFSPreOrder(binaryTree.RightChild, list);
            }
            return binaryTree;
        }

        public void ForEachInOrder(Action<T> action)
        {
            DFSForEach(this, action);
        }

        private void DFSForEach(IAbstractBinaryTree<T> tree, Action<T> action)
        {
            if (tree.LeftChild != null)
            {
                DFSForEach(tree.LeftChild, action);
            }
            action(tree.Value);
            if (tree.RightChild != null)
            {
                DFSForEach(tree.RightChild, action);
            }
        }
    }
}
