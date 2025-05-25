namespace com.zeroerror.behaviortree.Runtime
{
    public class SequenceNodeData : CompositeNodeData
    {
        public SequenceNodeData() : base()
        {
            this.nodeName = "顺序节点";
        }

        public override NodeData Clone()
        {
            var nodeData = new SequenceNodeData();
            nodeData.CopyFrom(this);
            return nodeData;
        }

        public override Node ToNode()
        {
            throw new System.NotImplementedException();
        }
    }
}