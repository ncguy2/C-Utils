namespace Utils.Tree {
    public interface IVisitor<T> {
        IVisitor<T> Visit(IVisitable<T> visitable);
        void VisitData(IVisitable<T> visitable, T data);
    }
}