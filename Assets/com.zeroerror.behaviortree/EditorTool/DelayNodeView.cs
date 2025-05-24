using UnityEditor;
using UnityEngine;
using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.EditorTool
{
    public class DelayNodeView : DecoratorNodeView
    {
        protected override NodeData _InitNode()
        {
            var nodeData = new DelayNodeData();
            return nodeData;
        }
    }
}