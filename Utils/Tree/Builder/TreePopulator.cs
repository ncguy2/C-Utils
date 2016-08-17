using System;
using System.Collections.Generic;

namespace Utils.Tree.Builder {
    public class TreePopulator {

        public static void Populate<T, U>(VisitableTree<T> tree, List<U> data, char delim, Func<U, String> stringLoc,
            Func<U, String, T> nodeLoc) {
            Populate(tree, data, delim, stringLoc, nodeLoc, false);
        }

        public static void Populate<T, U>(VisitableTree<T> tree, List<U> data, char delim, Func<U, String> stringLoc,
            Func<U, String, T> nodeLoc, bool fullPath) {
            VisitableTree<T> current = tree;
            foreach (U u in data) {
                VisitableTree<T> root = current;
                String pathStr = stringLoc.Invoke(u);
                String[] path = pathStr.Split(delim);
                String p = "";
                foreach (String cat in path) {
                    if (fullPath) {
                        if (p.Length == 0) p = cat;
                        else p += delim + cat;
                    }else p = cat;
                    current = current.Child(nodeLoc.Invoke(u, p));
                }
                current = root;
            }
        }

    }
}