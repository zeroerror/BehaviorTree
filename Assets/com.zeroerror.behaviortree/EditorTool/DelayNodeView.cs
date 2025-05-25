using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.EditorTool
{
    [NodeMenu("装饰节点/延迟节点")]
    public class DelayNodeView : DecoratorNodeView
    {
        protected override NodeData _InitNode()
        {
            var nodeData = new DelayNodeData();
            return nodeData;
        }

        protected override void _Draw()
        {
            var data = this.nodeData as DelayNodeData;
            data.delayTime = UnityEditor.EditorGUILayout.FloatField("延迟时间", data.delayTime);
        }
    }
}