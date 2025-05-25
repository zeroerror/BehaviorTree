using com.zeroerror.behaviortree.EditorTool;
using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.Sample.EditorTool
{
    [NodeMenu("行为节点/日志节点")]
    public class LogActionNodeView : ActionNodeView
    {
        protected override void _Draw()
        {
            var data = this.nodeData as LogActionNodeData;
            data.message = UnityEditor.EditorGUILayout.TextField("日志内容", data.message);
        }

        protected override NodeData _InitNode()
        {
            var node = new LogActionNodeData();
            return node;
        }
    }
}