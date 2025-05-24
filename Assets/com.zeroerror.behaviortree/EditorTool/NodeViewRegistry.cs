using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
namespace com.zeroerror.behaviortree.EditorTool
{
    public static class NodeViewRegistry
    {
        public static List<(string menuName, Type nodeViewType)> GetAllNodeViewsWithMenu()
        {
            var result = new List<(string, Type)>();
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                foreach (var type in assembly.GetTypes())
                {
                    var attr = type.GetCustomAttribute<NodeMenuAttribute>();
                    if (attr != null && typeof(NodeView).IsAssignableFrom(type))
                    {
                        result.Add((attr.MenuName, type));
                    }
                }
            }
            return result;
        }
    }
}