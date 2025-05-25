using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.EditorTool
{
    [NodeMenu("复合节点/顺序节点")]
    public class SequenceNodeView : CompositeNodeView
    {
        protected override NodeData _InitNode()
        {
            var nodeData = new SequenceNodeData();
            return nodeData;
        }
    }
}