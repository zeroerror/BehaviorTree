using System;
namespace com.zeroerror.behaviortree.EditorTool
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class NodeMenuAttribute : Attribute
    {
        public string MenuName { get; }
        public NodeMenuAttribute(string menuName)
        {
            MenuName = menuName;
        }
    }
}