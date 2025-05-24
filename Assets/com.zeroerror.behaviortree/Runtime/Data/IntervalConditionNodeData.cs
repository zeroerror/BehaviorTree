namespace com.zeroerror.behaviortree.Runtime
{
    [System.Serializable]
    public class IntervalConditionNodeData : NodeData
    {
        public float interval = 1f;

        public IntervalConditionNodeData() : base()
        {
            this.nodeName = "间隔时间";
        }

        public override NodeData Clone()
        {
            var nodeData = new IntervalConditionNodeData();
            nodeData.CopyFrom(this);
            nodeData.interval = this.interval;
            return nodeData;
        }
    }
}