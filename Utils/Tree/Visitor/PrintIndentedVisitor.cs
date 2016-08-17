using System;

namespace Utils.Tree.Visitor
{
    public class PrintIndentedVisitor<T> : IVisitor<T> {

        private readonly int _indent;
        private readonly Action<String> _printAction;

        public PrintIndentedVisitor(int indent, Action<String> printAction) {
            this._indent = indent;
            this._printAction = printAction;
        }

        public IVisitor<T> Visit(IVisitable<T> visitable) {
            return new PrintIndentedVisitor<T>(this._indent + 2, this._printAction);
        }

        public void VisitData(IVisitable<T> visitable, T data) {
            for (int i = 0; i < this._indent; i++)
                this._printAction.Invoke(" ");
            this._printAction.Invoke(data.ToString()+"\n");
        }
    }
}