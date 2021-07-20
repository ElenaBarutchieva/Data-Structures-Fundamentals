namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            this.nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            Tree<int> root = null;
            for (int i = 0; i < input.Length; i++)
            {
                var current = input[i].Split();
                var parentKey = int.Parse(current[0]);
                var key = int.Parse(current[1]);

                var tree = new Tree<int>(key);
                nodesBykeys.Add(key, tree);
                if (!nodesBykeys.ContainsKey(parentKey))
                {
                    var findedRoot = new Tree<int>(parentKey);
                    nodesBykeys.Add(parentKey, findedRoot);
                    root = findedRoot;
                }
                tree.AddParent(nodesBykeys[parentKey]);
                nodesBykeys[parentKey].AddChild(tree);
            }
            return root;
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            var tree = new Tree<int>(key);
            this.nodesBykeys.Add(key, tree);
            return tree;
        }

        public void AddEdge(int parent, int child)
        {
            throw new NotImplementedException();
        }

        private Tree<int> GetRoot()
        {
            throw new NotImplementedException();
        }
    }
}
