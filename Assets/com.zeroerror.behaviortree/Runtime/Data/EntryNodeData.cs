namespace com.zeroerror.behaviortree.Runtime
{
    public class EntryNodeData : NodeData
    {
        public EntryNodeData() : base()
        {
            this.nodeName = "入口节点";
        }

        public override NodeData Clone()
        {
            var nodeData = new EntryNodeData();
            nodeData.CopyFrom(this);
            return nodeData;
        }

        public override Node ToNode()
        {
            var node = new EntryNode();
            return node;
        }

    }
}