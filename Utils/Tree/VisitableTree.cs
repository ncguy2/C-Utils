using System.Collections.Generic;

namespace Utils.Tree {
    public class VisitableTree<T> : IVisitable<T>
    {
        private HashSet<VisitableTree<T>> _children = new HashSet<VisitableTree<T>>();
        private T _data;

        public VisitableTree(T d) {
            this._data = d;
        }

        public void Accept(IVisitor<T> visitor) {
            visitor.VisitData(this, this.Data());
            foreach (VisitableTree<T> child in _children) {
                child.Accept(visitor.Visit(child));
            }
        }

        public T Data() {
            return this._data;
        }

        public VisitableTree<T> Child(T data) {
            foreach (VisitableTree<T> child in _children) {
                if (child.Data().Equals(data)) return child;
            }
            return Child(new VisitableTree<T>(data));
        }

        public VisitableTree<T> Child(VisitableTree<T> child) {
            _children.Add(child);
            return child;
        }

    }
}