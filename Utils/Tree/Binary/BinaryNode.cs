using System;
using System.Collections.Generic;
using Utils.Tree.Mutable;

namespace Utils.Tree.Binary
{
    public class BinaryNode<T> : IVisitable<T> {
        private BinaryNode<T> _left;
        private BinaryNode<T> _right;

        private readonly BinaryTree<T> _parentTree;

        public BinaryNode(BinaryTree<T> parentTree, T data) {
            this._parentTree = parentTree;
            this.Data = data;
        }

        public BinaryNode<T> GetLeft() {
            return this._left;
        }
        public BinaryNode<T> GetRight() {
            return this._right;
        }

        public void SetLeft(BinaryNode<T> left) {
            this._left = left;
        }
        public void SetRight(BinaryNode<T> right) {
            this._right = right;
        }

        public void SetLeft(T left) {
            SetLeft(new BinaryNode<T>(_parentTree, left));
        }
        public void SetRight(T right) {
            SetRight(new BinaryNode<T>(_parentTree, right));
        }

        public T Data { get; set; }

        public void Add(T e) {
            int offset = this._parentTree.Compare(e, Data);
            if(offset < 0) AddLeft(e);
            else if (offset > 0) AddRight(e);
        }

        private void AddLeft(T e) {
            if (_left == null) SetLeft(e);
            else _left.Add(e);
        }

        private void AddRight(T e) {
            if (_right == null) SetRight(e);
            else _right.Add(e);
        }

        public void Flatten(List<T> list) {
            _left?.Flatten(list);
            list.Add(Data);
            _right?.Flatten(list);
        }

        public int Contains(T element) {
            if (Data.Equals(element)) return 1;

            int leftDepth = 0;
            if (_left != null) leftDepth = _left.Contains(element);
            if (leftDepth > 0) return leftDepth + 1;

            int rightDepth = 0;
            if (_right != null) rightDepth = _right.Contains(element);
            if (rightDepth > 0) return rightDepth + 1;

            return 0;
        }

        public void Accept(IVisitor<T> visitor) {
            visitor.VisitData(this, Data);
            _left?.Accept(visitor.Visit(_left));
            _right?.Accept(visitor.Visit(_right));
        }

        T IVisitable<T>.Data() {
            return Data;
        }

        public int BalanceFactor() {
            int lDesc = _left?.Descendants() ?? 0;
            int rDesc = _right?.Descendants() ?? 0;
            return -lDesc + rDesc;
        }

        public int Descendants() {
            int descendants = 0;
            if(_left != null)  descendants += _left.Descendants();
            if(_right != null) descendants += _right.Descendants();
            return descendants;
        }

        public void Balance() {
            int factor = BalanceFactor();
            if (IsBalanced(factor)) return;
            
        }

        public bool IsBalanced() { return IsBalanced(BalanceFactor()); }

        public bool IsBalanced(int factor) {
            return factor >= -1 && factor <= 1;
        }

        public void Clear() {
            _left?.Clear();
            _right?.Clear();
            _left = null;
            _right = null;
        }
    }
}