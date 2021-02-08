using System;
using System.Linq;

namespace io.vlingo.xoom.codegen.content {
    public class ClassFormatter {

        public static string qualifiedNameOf(string packageName, string className) {
            return string.Concat(packageName, ".", className);
        }

        public static string simpleNameToAttribute(string simpleName) {
            return simpleName.Length == 1 ? simpleName.ToLowerInvariant() : simpleName.All(x => Char.IsUpper(x)) ? simpleName : string.Concat(simpleName.Substring(0, 1).ToLowerInvariant(), simpleName.Substring(1, simpleName.Length - 1));
        }

        public static string qualifiedNameToAttribute(string qualifiedName) {
            return simpleNameToAttribute(simpleNameOf(qualifiedName));
        }

        public static string simpleNameOf(string qualifiedName) {
            return qualifiedName.Substring(qualifiedName.LastIndexOf(".") + 1);
        }

        public static string packageOf(string qualifiedName) {
            return qualifiedName.Substring(0, qualifiedName.LastIndexOf("."));
        }

    }
}
