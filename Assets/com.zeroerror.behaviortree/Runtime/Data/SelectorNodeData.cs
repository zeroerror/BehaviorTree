namespace com.zeroerror.behaviortree.Runtime
{
    public class SelectorNodeData : CompositeNodeData
    {
        public SelectorNodeData() : base()
        {
            this.nodeName = "复合节点 - 选择器";
        }

        public override NodeData Clone()
        {
            var nodeData = new SelectorNodeData();
            nodeData.CopyFrom(this);
            return nodeData;
        }

        public override Node ToNode()
        {
            var node = new SelectorNode();
            return node;
        }
    }
}