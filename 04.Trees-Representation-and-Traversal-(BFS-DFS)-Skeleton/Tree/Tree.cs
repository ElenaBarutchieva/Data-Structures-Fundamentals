namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();

        public bool IsRootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();
            if (IsRootDeleted)
            {
                return result;
            }

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                Tree<T> subtree = queue.Dequeue();

                result.Add(subtree.Value);

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();
            this.DFS(this, result);

            return result;
        }

        private void DFS(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree.Children)
            {
                this.DFS(child, result);
            }

            result.Add(tree.Value);
        }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var searchedNode = this.FindBfs(parentKey);

            if (searchedNode == null)
            {
                throw new ArgumentNullException();
            }

            searchedNode._children.Add(child);
        }

        private Tree<T> FindBfs(T parentKey)
        {
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);

            while (queue.Count != 0)
            {
                Tree<T> subtree = queue.Dequeue();
                if (subtree.Value.Equals(parentKey))
                {
                    var result = subtree;
                    return result;
                }

                foreach (var child in subtree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }

        public void RemoveNode(T nodeKey)
        {
            var searchedNode = this.FindBfs(nodeKey);

            if (searchedNode == null)
            {
                throw new ArgumentNullException();
            }

            foreach (var child in searchedNode.Children)
            {
                child.Parent = null;
            }

            searchedNode._children.Clear();
            Tree<T> serachedParent = searchedNode.Parent;
            if (serachedParent == null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                serachedParent._children.Remove(searchedNode);
            }

            searchedNode.Value = default(T);
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstSearchedNode = this.FindBfs(firstKey);
            var secondSearchedNode = this.FindBfs(secondKey);

            if (firstSearchedNode == null || secondSearchedNode == null)
            {
                throw new ArgumentNullException();
            }

            var firstParent = firstSearchedNode.Parent;
            var secondParent = secondSearchedNode.Parent;

            if (firstParent == null)
            {
                firstSearchedNode.Value = secondSearchedNode.Value;
                firstSearchedNode._children = secondSearchedNode._children;
                secondSearchedNode.Parent = null;
                return;
            }
            if (secondParent == null)
            {
                secondSearchedNode.Value = firstSearchedNode.Value;
                secondSearchedNode._children = firstSearchedNode._children;
                firstSearchedNode.Parent = null;
                return;
            }


            firstSearchedNode.Parent = secondParent;
            secondSearchedNode.Parent = firstParent;

            int indexOfFirst = firstParent._children.IndexOf(firstSearchedNode);
            int indexOfSecond = secondParent._children.IndexOf(secondSearchedNode);

            firstParent._children[indexOfFirst] = secondSearchedNode;
            secondParent._children[indexOfSecond] = firstSearchedNode;
        }

    }
}
