using System;
using System.Collections.Generic;

namespace Utils.Tree.Builder {
    public class TreePopulator {

        public static void Populate<U>(VisitableTree<TreeObjectWrapper<U>> tree, List<U> data, char delim, Func<U, String> stringLoc) {
            Func<U, String, TreeObjectWrapper<U>> nodeLoc = (u, s) => {
                if(s.Equals(stringLoc.Invoke(u))) return new TreeObjectWrapper<U>(u, s);
                return new TreeObjectWrapper<U>(s);
            };
            Populate(tree, data, delim, stringLoc, nodeLoc);
        }

        public static void Populate<U>(VisitableTree<TreeObjectWrapper<U>> tree, List<U> data, char delim, Func<U, String> stringLoc,
            Func<U, String, TreeObjectWrapper<U>> nodeLoc) {
            Populate(tree, data, delim, stringLoc, nodeLoc, false);
        }

        public static void Populate<U>(VisitableTree<TreeObjectWrapper<U>> tree, List<U> data, char delim, Func<U, String> stringLoc,
            Func<U, String, TreeObjectWrapper<U>> nodeLoc, bool fullPath) {
            VisitableTree<TreeObjectWrapper<U>> current = tree;
            foreach (U u in data) {
                VisitableTree<TreeObjectWrapper<U>> root = current;
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