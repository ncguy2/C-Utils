namespace Utils.Tree.Mutable {
    public class MutableWrapper<T> {

        private T value;

        public MutableWrapper() {
            value = default(T);
        }

        public MutableWrapper(T value) {
            this.value = value;
        }

        public void SetValue(T value) {
            this.value = value;
        }

        public T GetValue() {
            return this.value;
        }

    }
}