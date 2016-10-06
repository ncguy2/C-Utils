using System;
using System.Collections.Generic;

namespace Utils.Tree.Binary
{
    public class BinaryTree<T> {
        private BinaryNode<T> _rootNode;
        private Func<T, T, Int32> _comparator;

        public BinaryTree(T rootData, Func<T, T, Int32> comparator) {
            this._rootNode = new BinaryNode<T>(this, rootData);
            this._comparator = comparator;
        }

        public void Add(params T[] data) {
            foreach (T t in data)
                Add(_rootNode, t);
        }

        public void Add(T data) {
            Add(_rootNode, data);
        }

        public void Add(BinaryNode<T> node, T data) {
            node.Add(data);
        }

        public int Compare(T data, T host) {
            return _comparator.Invoke(data, host);
        }

        public List<T> Flatten() {
            List<T> nodes = new List<T>();
            _rootNode.Flatten(nodes);
            return nodes;
        }

        public int Contains(T element) {
            return _rootNode.Contains(element);
        }

        public void Accept(IVisitor<T> visitor) {
            _rootNode.Accept(visitor);
        }

        public void Balance() {

        }

        public void Remove(T element) {
            List<T> flat = Flatten();
            flat.Remove(element);
            _rootNode.Clear();
            flat.ForEach(_rootNode.Add);
        }

    }
}