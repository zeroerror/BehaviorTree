namespace com.zeroerror.behaviortree.Runtime
{
    public class DelayNodeData : DecoratorNodeData
    {
        public float delayTime;

        public DelayNodeData() : base()
        {
            this.nodeName = "延迟时间";
        }

        public override NodeData Clone()
        {
            var nodeData = new DelayNodeData();
            nodeData.CopyFrom(this);
            nodeData.delayTime = this.delayTime;
            return nodeData;
        }

        public override Node ToNode()
        {
            // todo 
            var node = new DelayNode(null, this.delayTime);
            return node;
        }
    }
}