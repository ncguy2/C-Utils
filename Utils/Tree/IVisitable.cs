namespace Utils.Tree {
    public interface IVisitable<T> {
        void Accept(IVisitor<T> visitor);
        T Data();
    }
}