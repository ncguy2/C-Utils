using System;

namespace Utils.Tree.Builder
{
    public class TreeObjectWrapper<T> {

        private T _object;
        private String _label;

        public TreeObjectWrapper(String label) {
            this._label = label;
        }

        public TreeObjectWrapper(T obj, String label) {
            this._object = obj;
            this._label = label;
        }

        public override string ToString() {
            if (_object == null)
                return _label;
            return _object.ToString();
        }

        public override bool Equals(object obj) {
            if (this == obj) return true;
            if(this._object != null)
                if (this._object.Equals(_object))
                    return true;
            if (this._label.Equals(obj)) return true;
            if (obj is TreeObjectWrapper<T>) {
                TreeObjectWrapper<T> o = (TreeObjectWrapper<T>) obj;
                if(this._object != null) if (this._object.Equals(o._object)) return true;
                if (this._label.Equals(o._label)) return true;
            }
            return base.Equals(obj);
        }

    }
}