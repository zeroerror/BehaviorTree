using com.zeroerror.behaviortree.Runtime;
using UnityEditor;
using UnityEngine;

namespace com.zeroerror.behaviortree.EditorTool
{
    [NodeMenu("行为节点/日志打印")]
    public class LogActionNodeView : NodeView
    {
        protected override void _Draw()
        {
            GUILayout.Label(title, EditorStyles.boldLabel);
        }

        protected override NodeData _InitNode()
        {
            var nodeData = new LogActionNodeData();
            return nodeData;
        }
    }
}