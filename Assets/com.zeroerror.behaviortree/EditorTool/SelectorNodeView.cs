using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.EditorTool
{
    [NodeMenu("复合节点/选择节点")]
    public class SelectorNodeView : CompositeNodeView
    {
        protected override NodeData _InitNode()
        {
            var nodeData = new SelectorNodeData();
            return nodeData;
        }
    }
}