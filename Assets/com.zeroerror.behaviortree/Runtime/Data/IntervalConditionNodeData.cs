namespace com.zeroerror.behaviortree.Runtime
{
    [System.Serializable]
    public class IntervalConditionNodeData : ConditionData
    {
        public float interval = 1f;

        public IntervalConditionNodeData() : base()
        {
            this.nodeName = "条件节点 - 延迟时间";
        }

        public override NodeData Clone()
        {
            var nodeData = new IntervalConditionNodeData();
            nodeData.CopyFrom(this);
            nodeData.interval = this.interval;
            return nodeData;
        }

        public override Node ToNode()
        {
            var node = new IntervalConditionNode(interval);
            return node;
        }

    }
}