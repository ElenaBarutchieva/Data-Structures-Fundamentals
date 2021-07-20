namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T>
    {
        private List<Tree<T>> _children;

        //Ready
        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children = new List<Tree<T>>(children);
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        //Ready
        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        //Ready
        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }
        //Ready
        public string GetAsString()
        {
            StringBuilder result = new StringBuilder();

            DFS(this, result, 0);
            return result.ToString().Trim();
        }

        //GetTreeAsString
        //Ready
        private void DFS(Tree<T> tree, StringBuilder result, int spaces)
        {
            result.AppendLine(new string(' ', spaces) + tree.Key);
            foreach (var child in tree.Children)
            {
                this.DFS(child, result, spaces + 2);
            }
        }

        //Ready
        private void MiddleLeafDFS(Tree<T> tree, List<T> result)
        {
            if (tree._children.Count != 0 && tree.Parent != null)
            {
                result.Add(tree.Key);
            }
            foreach (var child in tree.Children)
            {
                this.MiddleLeafDFS(child, result);
            }
        }

        //GetLeafsNodes
        //Ready
        private void DFS(Tree<T> tree, List<T> result)
        {
            if (tree._children.Count == 0)
            {
                result.Add(tree.Key);
            }
            foreach (var child in tree.Children)
            {
                this.DFS(child, result);
            }

        }

        //Ready
        //DeepestNode
        private void DFS(Tree<T> tree, ref int maxCount, int count, ref Tree<T> result)
        {
            foreach (var child in tree.Children)
            {
                this.DFS(child, ref maxCount, count + 1, ref result);
            }

            if (count > maxCount)
            {
                maxCount = count;
                result = tree;
            }
        }

        //Ready
        public Tree<T> GetDeepestLeftomostNode()
        {
            Tree<T> result = null;
            int maxCount = int.MinValue;
            this.DFS(this, ref maxCount, 0, ref result);

            return result;
        }

        //ready
        public List<T> GetLeafKeys()
        {
            var result = new List<T>();
            this.DFS(this, result);
            result.Sort();
            return result;
        }

        //Ready
        public List<T> GetMiddleKeys()
        {
            var result = new List<T>();
            this.MiddleLeafDFS(this, result);
            result.Sort();
            return result;
        }

        //Ready
        public List<T> GetLongestPath()
        {
            var result = new List<T>();
            Tree<T> deepestLeaf = this.GetDeepestLeftomostNode();

            while (deepestLeaf != null)
            {
                result.Add(deepestLeaf.Key);
                deepestLeaf = deepestLeaf.Parent;
            }
            result.Reverse();
            return result;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var result = new List<List<T>>();
            var first = new List<T>();
            this.PathsSumDFS(this, ref result, first, sum);

            return result;
        }
        //Ready
        private void PathsSumDFS(Tree<T> tree, ref List<List<T>> result, List<T> current, int sum)
        {
            current.Add(tree.Key);
            foreach (var child in tree.Children)
            {
                //current.Add(child.Key);
                this.PathsSumDFS(child, ref result, current, sum);
            }

            if (current.Select(x => int.Parse(x.ToString())).Sum() == sum)
            {
                result.Add(new List<T>(current));
            }

            current.Remove(tree.Key);
            
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var result = new List<Tree<T>>();
            this.SubtreesDFS(this, result, sum);

            return result;
        }

        private int SubtreesDFS(Tree<T> tree, List<Tree<T>> result, int sum)
        {
            int current = Convert.ToInt32(tree.Key);
            foreach (var child in tree.Children)
            {
                current += this.SubtreesDFS(child, result, sum);
            }

            if (current == sum)
            {
                result.Add(tree);
            }
            return current;
        }


    }
}
