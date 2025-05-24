namespace com.zeroerror.behaviortree.Runtime
{
    [System.Serializable]
    public class LogActionNodeData : NodeData
    {
        public LogActionNodeData() : base()
        {
            this.nodeName = "日志打印";
        }

        public override NodeData Clone()
        {
            var nodeData = new LogActionNodeData();
            nodeData.CopyFrom(this);
            return nodeData;
        }
    }
}