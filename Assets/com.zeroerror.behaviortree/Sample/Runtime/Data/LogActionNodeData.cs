using com.zeroerror.behaviortree.Runtime;

namespace com.zeroerror.behaviortree.Sample
{
    [System.Serializable]
    public class LogActionNodeData : ActionNodeData
    {
        public string message;

        public LogActionNodeData() : base()
        {
            this.nodeName = "行为节点 - 日志节点";
        }

        public override NodeData Clone()
        {
            var nodeData = new LogActionNodeData();
            nodeData.CopyFrom(this);
            nodeData.message = this.message;
            return nodeData;
        }

        public override Node ToNode()
        {
            var node = new LogActionNode(message);
            return node;
        }

    }
}