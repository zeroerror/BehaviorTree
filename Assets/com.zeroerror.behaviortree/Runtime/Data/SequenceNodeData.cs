namespace com.zeroerror.behaviortree.Runtime
{
    public class SequenceNodeData : CompositeNodeData
    {
        public SequenceNodeData() : base()
        {
            this.nodeName = "复合节点 - 顺序执行";
        }

        public override NodeData Clone()
        {
            var nodeData = new SequenceNodeData();
            nodeData.CopyFrom(this);
            return nodeData;
        }

        public override Node ToNode()
        {
            var node = new SequenceNode();
            return node;
        }
    }
}