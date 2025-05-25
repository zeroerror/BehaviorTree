namespace com.zeroerror.behaviortree.Runtime
{
    public class DelayNodeData : DecoratorNodeData
    {
        public float delayTime;

        public DelayNodeData() : base()
        {
            this.nodeName = "装饰节点 - 延迟时间";
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
            var node = new DelayNode(delayTime);
            return node;
        }

    }
}